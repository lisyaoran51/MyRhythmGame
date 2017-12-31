using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Graphics {
    public abstract class FlowContainer<T> 
        where T : Drawable {

        protected bool hasNewLayout = false;

        public List<T> Drawables {
            private set; get;
        }

        public FlowContainer() {
            Drawables = new List<T>();
        }

        protected abstract IEnumerable<Vector2> ComputeLayoutPositions();

        public void Update() {
            if (hasNewLayout)
                performLayout();
        }

        private void performLayout() {
            //OnLayout?.Invoke();

            if (!Drawables.Any())
                return;

            var positions = ComputeLayoutPositions().ToArray();

            int i = 0;
            foreach (var d in Drawables) {
                if (i > positions.Length)
                    throw new InvalidOperationException(
                        GetType().FullName + " returned a total of " + positions.Length + 
                        " positions for " + i + " children. ComputeLayoutPositions() must return 1 position per child.");

                // In some cases (see the right hand side of the conditional) we want to permit relatively sized children
                // in our flow direction; specifically, when children use FillMode.Fit to preserve the aspect ratio.
                // Consider the following use case: A flow container has a fixed width but an automatic height, and flows
                // in the vertical direction. Now, we can add relatively sized children with FillMode.Fit to make sure their
                // aspect ratio is preserved while still allowing them to flow vertically. This special case can not result
                // in an autosize-related feedback loop, and we can thus simply allow it.
                /*
                if ((d.RelativeSizeAxes & AutoSizeAxes) != 0 && (d.FillMode != FillMode.Fit || d.RelativeSizeAxes != Axes.Both || d.Size.X > RelativeChildSize.X || d.Size.Y > RelativeChildSize.Y || AutoSizeAxes == Axes.Both))
                    throw new InvalidOperationException(
                        "Drawables inside a flow container may not have a relative size axis that the flow container is auto sizing for." +
                        $"The flow container is set to autosize in {AutoSizeAxes} axes and the child is set to relative size in {d.RelativeSizeAxes} axes.");

                if (d.RelativePositionAxes != Axes.None)
                    throw new InvalidOperationException($"A flow container cannot contain a child with relative positioning (it is {d.RelativePositionAxes}).");
                */
                var finalPos = positions[i];
                if ((Vector2)(d.transform.localPosition) != finalPos)
                    // d.MoveTo(finalPos, LayoutDuration, LayoutEasing); //未來再來寫easing
                    d.transform.localPosition = finalPos;

                ++i;
            }

            if (i != positions.Length)
                throw new InvalidOperationException(
                    GetType().FullName + " ComputeLayoutPositions() returned a total of " + positions.Length + 
                    " positions for " + i + " children. ComputeLayoutPositions() must return 1 position per child.");
        }

        internal void Add(T drawable) {
            Drawables.Add(drawable);
            hasNewLayout = true;
        }
    }
}