﻿using Base.Rulesets.Objects;
using Base.Rulesets.Objects.Drawables;
using Base.Sheetmusics;
using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI {

    public abstract class RulesetContainer : ChildAddable {
        public Ruleset Ruleset;
    }

    public abstract class RulesetContainer<TObject> : RulesetContainer
        where TObject : HitObject {

        public event Action<Judgement> OnJudgement;

        public Sheetmusic<TObject> Sheetmusic;

        public PlayField PlayField;

        public WorkingSheetmusic WorkingSheetmusic;



        /// <summary>
        /// Creates a converter to convert Beatmap to a specific mode.
        /// </summary>
        /// <returns>The Beatmap converter.</returns>
        protected abstract SheetmusicConverter<TObject> CreateSheetmusicConverter();

        /// <summary>
        /// Creates a processor to perform post-processing operations
        /// on HitObjects in converted Beatmaps.
        /// </summary>
        /// <returns>The Beatmap processor.</returns>
        protected virtual SheetmusicProcessor<TObject> CreateSheetmusicProcessor() {
            return new SheetmusicProcessor<TObject>();
        }

        //TODO:RulesetContainer.construct
        public void construct(WorkingSheetmusic workingSheetmusic, bool isForCurrentRuleset = true) {
            WorkingSheetmusic = workingSheetmusic;

            SheetmusicConverter<TObject> converter = CreateSheetmusicConverter();
            SheetmusicProcessor<TObject> processor = CreateSheetmusicProcessor();


            Sheetmusic = converter.Convert(WorkingSheetmusic.Sheetmusic);
            processor.PostProcess(Sheetmusic);
        }

        public void Load() {
            PlayField = CreatePlayfield();
            loadObjects();
        }

        

        // TODO: RulesetContainer:CreatePlayfield
        protected abstract PlayField CreatePlayfield();

        private void loadObjects() {
            foreach(TObject h in Sheetmusic.HitObjects) {
                DrawableHitObject < TObject > drawableHitObject = GetVisualRepresentation(h);

                // 當hitObject的onJudgement觸發時，連貫觸發playField和RulesetCntainer的Onjudgement
                drawableHitObject.OnJudgement += (d, j) => 
                {
                    PlayField.OnJudgement(d, j);
                    OnJudgement.Invoke(j);
                };
                PlayField.Add(drawableHitObject);
            }

            PlayField.PostProcess();
        }
        

        protected abstract DrawableHitObject<TObject> GetVisualRepresentation(TObject hitObject);

        //TODO:RulesetContainer.CreateScoreProcessor
        public ScoreProcessor CreateScoreProcessor() {
            return null;
        }
    }

    public abstract class RulesetContainer<TPlayField, TObject> : RulesetContainer<TObject>
        where TPlayField : PlayField
        where TObject : HitObject
    {
        protected new TPlayField PlayField;

        public new void construct(WorkingSheetmusic workingSheetMusic, bool isForCurrentRuleset) {
            PlayField = (TPlayField)base.PlayField;
        }
    }
}