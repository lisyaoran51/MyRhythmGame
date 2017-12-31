using Base.Graphics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Graphics {


    /// <summary>
    /// A <see cref="FlowContainer{Drawable}"/> that fills space by arranging its children
    /// next to each other.
    /// <see cref="Container{T}.Children"/> can be arranged horizontally, vertically, and in a
    /// combined fashion, which is controlled by <see cref="Direction"/>.
    /// <see cref="Container{T}.Children"/> are arranged from left-to-right if their
    /// <see cref="Drawable.Anchor"/> is to the left or centered horizontally.
    /// They are arranged from right-to-left otherwise.
    /// <see cref="Container{T}.Children"/> are arranged from top-to-bottom if their
    /// <see cref="Drawable.Anchor"/> is to the top or centered vertically.
    /// They are arranged from bottom-to-top otherwise.
    /// If non-<see cref="Drawable"/> <see cref="Container{T}.Children"/> are desired, use
    /// <see cref="FillFlowContainer{T}"/>.
    /// </summary>
    public class FillFlowContainer<T> : FlowContainer<T>
        where T : Drawable 
    {

        private FillDirection direction = FillDirection.Full;

        public FillFlowContainer(FillDirection fillDirection)
            : base() 
        {
            direction = fillDirection;
        }


        /// <summary>
        /// If <see cref="FillDirection.Full"/> or <see cref="FillDirection.Horizontal"/>,
        /// <see cref="Container{T}.Children"/> are arranged from left-to-right if their
        /// <see cref="Drawable.Anchor"/> is to the left or centered horizontally.
        /// They are arranged from right-to-left otherwise.
        /// If <see cref="FillDirection.Full"/> or <see cref="FillDirection.Vertical"/>,
        /// <see cref="Container{T}.Children"/> are arranged from top-to-bottom if their
        /// <see cref="Drawable.Anchor"/> is to the top or centered vertically.
        /// They are arranged from bottom-to-top otherwise.
        /// </summary>
        public FillDirection Direction {
            get { return direction; }
            set {
                if (direction == value)
                    return;

                direction = value;
                //InvalidateLayout();
            }
        }

        private Vector2 spacing;

        /// <summary>
        /// The spacing between individual elements. Default is <see cref="Vector2.Zero"/>.
        /// </summary>
        public Vector2 Spacing {
            get { return spacing; }
            set {
                if (spacing == value)
                    return;

                spacing = value;
                hasNewLayout = true;
                //InvalidateLayout();
            }
        }

        private Vector2 spacingFactor(Drawable c) {
            Vector2 result = c.Pivot;
            float x = result.x, y = result.y;
            if (c.Pivot.x == 1)
                x = 1 - result.x;
            if (c.Pivot.y == 1)
                y = 1 - result.y;
            return new Vector2(x,y);
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

                //We've exceeded our allowed width, move to a new row
                if (direction != FillDirection.Horizontal /*&& (Precision.DefinitelyBigger(rowWidth, max.X) || direction == FillDirection.Vertical)*/) {
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
                if (i < drawables.Length - 1) {
                    // Compute stride. Note, that the stride depends on the origins of the drawables
                    // on both sides of the step to be taken.
                    stride = new Vector2((1 - spacingFactor(c).x) * size.x,
                                         (1 - spacingFactor(c).y) * size.y);

                    c = drawables[i + 1];
                    size = c.Bounds.size;

                    stride += new Vector2(spacingFactor(c).x * size.x,
                                          spacingFactor(c).y * size.y);
                }

                stride += Spacing;

                if (stride.y > rowHeight)
                    rowHeight = stride.y;
                current.x += stride.x;
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

                if (c.Pivot.x == 0.5)
                    // Begin flow at centre of row
                    result[i].x += rowOffsetsToMiddle[rowIndices[i]];
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

    /// <summary>
    /// Represents the horizontal direction of a fill flow.
    /// </summary>
    public enum FillDirection {
        /// <summary>
        /// Fill horizontally first, then fill vertically via multiple rows.
        /// </summary>
        Full,

        /// <summary>
        /// Fill only horizontally.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Fill only vertically.
        /// </summary>
        Vertical,
    }
}