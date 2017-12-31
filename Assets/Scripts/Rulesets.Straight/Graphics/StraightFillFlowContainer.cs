using Base.Graphics;
using Base.Rulesets.Straight.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Rulesets.Straight.Graphics {
    public class StraightFillFlowContainer : FillFlowContainer<Column> {

        public StraightFillFlowContainer() : base(FillDirection.Horizontal) {}


        private Vector2 spacingFactor(Drawable c) {
            Vector2 result = c.Pivot;
            float x = result.x, y = result.y;
            if (c.Pivot.x == 1)
                x = 1 - result.x;
            if (c.Pivot.y == 1)
                y = 1 - result.y;
            return new Vector2(x, y);
        }

        protected override IEnumerable<Vector2> ComputeLayoutPositions() {
            /* 螢幕最大大小，之後再用這段
            var max = MaximumSize;
            if (max == Vector2.Zero) {
                var s = ChildSize;

                // If we are autosize and haven't specified a maximum size, we should allow infinite expansion.
                // If we are inheriting then we need to use the parent size (our ActualSize).
                max.X = (AutoSizeAxes & Axes.X) > 0 ? float.MaxValue : s.X;
                max.Y = (AutoSizeAxes & Axes.Y) > 0 ? float.MaxValue : s.Y;
            }
            */

            var drawables = Drawables.ToArray();
            if (drawables.Length == 0)
                return new List<Vector2>();

            // The positions for each child we will return later on.
            Vector2[] result = new Vector2[drawables.Length];

            // We need to keep track of row widths such that we can compute correct
            // positions for horizontal centre anchor children.
            // We also store for each child to which row it belongs.
            int[] rowIndices = new int[drawables.Length];
            List<float> rowOffsetsToMiddle = new List<float> { 0 };

            // Variables keeping track of the current state while iterating over children
            // and computing initial flow positions.
            float rowHeight = 0;
            float rowBeginOffset = 0;
            var current = Vector2.zero;
            var whiteCurrent = Vector2.zero;

            // First pass, computing initial flow positions
            Vector2 size = Vector2.zero;
            for (int i = 0; i < drawables.Length; ++i) {
                Drawable c = drawables[i];

                // Populate running variables with sane initial values.
                if (i == 0) {
                    size = c.Bounds.size;
                    rowBeginOffset = spacingFactor(c).x * size.x;
                }

                float rowWidth = rowBeginOffset + current.x + (1 - spacingFactor(c).x) * size.x;
                /*
                 * 計算琴鍵位置，黑鍵寬度為白鍵的一半
                 *  1 3  6 8 10
                 * 0 2 45 7 9  11
                 */
                switch(i % 12) {
                    case 0:
                    case 5:
                        break;
                    case 1:
                        rowWidth = rowBeginOffset + current.x + 0.41666666f * size.x;
                        break;
                    case 2:
                    case 4:
                    case 7:
                    case 9:
                    case 11:
                        rowWidth = rowBeginOffset + whiteCurrent.x + (1 - spacingFactor(c).x) * size.x;
                        break;
                    case 3:
                        rowWidth = rowBeginOffset + current.x + 0.58333333f * size.x;
                        break;
                    case 6:
                        rowWidth = rowBeginOffset + current.x + 0.375f * size.x;
                        break;
                    case 8:
                        rowWidth = rowBeginOffset + current.x + 0.5f * size.x;
                        break;
                    case 10:
                        rowWidth = rowBeginOffset + current.x + 0.625f * size.x;
                        break;
                }

                //We've exceeded our allowed width, move to a new row
                if (Direction != FillDirection.Horizontal /*&& (Precision.DefinitelyBigger(rowWidth, max.X) || direction == FillDirection.Vertical)*/) {
                    /*
                    current.X = 0;
                    current.Y += rowHeight;

                    result[i] = current;

                    rowOffsetsToMiddle.Add(0);
                    rowBeginOffset = spacingFactor(c).X * size.X;

                    rowHeight = 0;
                    */
                } else {
                    result[i] = current;

                    // Compute offset to the middle of the row, to be applied in case of centre anchor
                    // in a second pass.
                    rowOffsetsToMiddle[rowOffsetsToMiddle.Count - 1] = rowBeginOffset - rowWidth / 2;
                }

                rowIndices[i] = rowOffsetsToMiddle.Count - 1;

                Vector2 stride = Vector2.zero;

                stride.x += Spacing.x;

                switch ((i+1) % 12) {
                    case 0:
                    case 5:
                        stride.x = size.x;
                        current += stride;
                        whiteCurrent = current;
                        break;
                    case 1:
                        stride.x = 0.41666666f * size.x;
                        current += stride;
                        break;
                    case 2:
                    case 4:
                    case 7:
                    case 9:
                    case 11:
                        stride.x = size.x;
                        whiteCurrent += stride;
                        current = whiteCurrent;
                        break;
                    case 3:
                        stride.x = 0.58333333f * size.x;
                        current += stride;
                        break;
                    case 6:
                        stride.x = 0.375f * size.x;
                        current += stride;
                        break;
                    case 8:
                        stride.x = 0.5f * size.x;
                        current += stride;
                        break;
                    case 10:
                        stride.x = 0.625f * size.x;
                        current += stride;
                        break;
                }
            }

            float height = result.Last().y;

            /*
             * 先不寫檢查每一個drawable的anchor是不是都相同
             */
            //Vector2 ourRelativeAnchor = drawables[0].RelativeAnchorPosition;

            // Second pass, adjusting the positions for anchors of children.
            // Uses rowWidths and height for centre-anchors.
            for (int i = 0; i < drawables.Length; ++i) {
                var c = drawables[i];
                /* 先不寫
                switch (Direction) {
                    case FillDirection.Vertical:
                        if (c.RelativeAnchorPosition.Y != ourRelativeAnchor.Y)
                            throw new InvalidOperationException(
                                $"All drawables in a {nameof(FillFlowContainer)} must use the same RelativeAnchorPosition for the given {nameof(FillDirection)}({Direction}) ({ourRelativeAnchor.Y} != {c.RelativeAnchorPosition.Y}). "
                                + $"Consider using multiple instances of {nameof(FillFlowContainer)} if this is intentional.");
                        break;
                    case FillDirection.Horizontal:
                        if (c.RelativeAnchorPosition.X != ourRelativeAnchor.X)
                            throw new InvalidOperationException(
                                $"All drawables in a {nameof(FillFlowContainer)} must use the same RelativeAnchorPosition for the given {nameof(FillDirection)}({Direction}) ({ourRelativeAnchor.X} != {c.RelativeAnchorPosition.X}). "
                                + $"Consider using multiple instances of {nameof(FillFlowContainer)} if this is intentional.");
                        break;
                    default:
                        if (c.RelativeAnchorPosition != ourRelativeAnchor)
                            throw new InvalidOperationException(
                                $"All drawables in a {nameof(FillFlowContainer)} must use the same RelativeAnchorPosition for the given {nameof(FillDirection)}({Direction}) ({ourRelativeAnchor} != {c.RelativeAnchorPosition}). "
                                + $"Consider using multiple instances of {nameof(FillFlowContainer)} if this is intentional.");
                        break;
                }
                */

                if (c.Pivot.x == 0.5);
                // Begin flow at centre of row
                // result[i].x += rowOffsetsToMiddle[rowIndices[i]];
                else if (c.Pivot.x == 1)
                    // Flow right-to-left
                    result[i].x = -result[i].x;

                if (c.Pivot.y == 0.5)
                    // Begin flow at centre of total height
                    result[i].y -= height / 2;
                else if (c.Pivot.x == 1)
                    // Flow bottom-to-top
                    result[i].y = -result[i].y;
            }

            return result;
        }
    }
}