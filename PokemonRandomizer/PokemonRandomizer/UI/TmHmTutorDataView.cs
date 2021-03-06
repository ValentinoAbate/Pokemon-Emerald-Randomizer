﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PokemonRandomizer.UI
{
    public class TmHmTutorDataView : DataView<TmHmTutorModel>
    {
        public TmHmTutorDataView(TmHmTutorModel model)
        {
            var stack = new StackPanel() { Orientation = Orientation.Vertical };
            Content = stack;
            stack.Add(new Label() { Content = "Randomization" });
            stack.Add(new Separator());
            stack.Add(new RandomChanceUI("Random Tm Moves", model.TMRandChance, (d) => model.TMRandChance = d, Orientation.Horizontal));
            stack.Add(new RandomChanceUI("Random Tutor Moves", model.MoveTutorRandChance, (d) => model.MoveTutorRandChance = d, Orientation.Horizontal));
            stack.Add(new BoundCheckBoxUI(model.NoHmMovesInTMsAndTutors, (b) => model.NoHmMovesInTMsAndTutors = b, "Prevent HM moves in TMs and Tutors"));
            stack.Add(new BoundCheckBoxUI(model.NoDuplicateTMsAndTutors, (b) => model.NoDuplicateTMsAndTutors = b, "Prevent duplicate moves in TMs and Tutors"));
            stack.Add(new BoundCheckBoxUI(model.KeepImportantTmsAndTutors, (b) => model.KeepImportantTmsAndTutors = b, "Keep important TMs and Tutors", "Ensures that important TMs and Tutors won't be randomized (Headbutt, Rock Smash, Secret Power, Flash, etc)"));
            stack.Add(new Separator());
            stack.Add(new Label() { Content = "Compatibility" });
            stack.Add(new Separator());
            stack.Add(new BoundComboBoxUI("TM Compatibility", TmHmTutorModel.CompatOptionDropdown, (int)model.TmCompatOption, (i) => model.TmCompatOption = (TmHmTutorModel.CompatOption)i));
            stack.Add(new BoundComboBoxUI("Tutor Compatibility", TmHmTutorModel.CompatOptionDropdown, (int)model.TutorCompatOption, (i) => model.TutorCompatOption = (TmHmTutorModel.CompatOption)i));
            stack.Add(new BoundComboBoxUI("HM Compatibility", TmHmTutorModel.CompatOptionDropdown, (int)model.HmCompatOption, (i) => model.HmCompatOption = (TmHmTutorModel.CompatOption)i));
            stack.Add(new BoundSliderUI("Random Compatibility On Chance", model.RandomCompatTrueChance, (d) => model.RandomCompatTrueChance = d));
            stack.Add(new BoundSliderUI("Intelligent Compatibiliy Noise", model.IntelligentCompatNoise, (d) => model.IntelligentCompatNoise = d, true, 0.01, 0, 0.33));
        }
    }
}
