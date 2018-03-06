using Base.Threading;
using Base.Utils;
using Base.Utils.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils {
    public abstract class Updatable : ChildAddable, IUpdatable {
        

        private event Action onUpdateSubTree;

        protected void Update() {
            UpdateSubTree();
        }
        
        private void update() {
            // no-op
        }

        public virtual void UpdateSubTree() {
            update();
            if (onUpdateSubTree != null)
                onUpdateSubTree.Invoke();
        }



        public void AddSubTree(Updatable subObject) {
            onUpdateSubTree += () => { subObject.UpdateSubTree(); };
        }

        public void AddSubTree(Updated subObject) {
            onUpdateSubTree += () => { subObject.UpdateSubTree(); };
        }
    }
}