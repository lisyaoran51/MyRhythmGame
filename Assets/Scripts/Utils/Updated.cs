using Base.Utils.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils {

    public abstract class Updated : IUpdatable {

        public bool IsLoaded { get { return loadState >= LoadState.Ready; } }

        private volatile LoadState loadState = LoadState.NotLoaded;

        private Updatable parent;

        public Updatable Parent {
            set {
                value.AddSubTree(this);
                parent = value;
                load();
                loadState = LoadState.Ready;
            }
            get {
                return parent;
            }
        }

        private Updated updatedParent;

        public Updated UpdatedParent {
            set {
                value.AddSubTree(this);
                updatedParent = value;
                load();
                loadState = LoadState.Ready;
            }
            get {
                return updatedParent;
            }
        }
        
        private event Action onUpdateSubTree;

        protected abstract void load();

        protected abstract void update();

        public virtual void UpdateSubTree() {
            if (!IsLoaded) return;

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