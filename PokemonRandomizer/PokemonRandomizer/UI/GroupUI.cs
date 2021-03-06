﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PokemonRandomizer.UI
{
    public class GroupUI<View, Model> where View : GroupDataView<Model>, new() where Model : DataModel, new()
    {
        private readonly TreeView groupSelectorTree;
        private readonly View dataView;
        public GroupUI(Panel parent, Panel viewParent, Model initialModel)
        {
            var verticalStack = new StackPanel() { Orientation = Orientation.Vertical };

            // Create and add the header GUI
            var headerStack = new StackPanel() { Orientation = Orientation.Horizontal };
            headerStack.Children.Add(new Label() { Content = "Groups" });
            var addButton = new Button() { Content = "Add", Height = 17, Width = 50, Margin = new Thickness(0, 0, 0, 1), FontSize = 11 };
            addButton.Click += AddButtonClick;
            headerStack.Children.Add(addButton);
            verticalStack.Children.Add(headerStack);

            // Create and add the tree view
            groupSelectorTree = new TreeView()
            { 
                Width = 100,
                Margin = new Thickness(0, 5, 5, 5)
            };
            var initialGroup = new Group(initialModel, OnClickGroup)
            {
                Header = "Group " + (groupSelectorTree.Items.Count + 1),
            };
            initialGroup.IsSelected = true;
            groupSelectorTree.Items.Add(initialGroup);
            verticalStack.Children.Add(groupSelectorTree);

            // Add these to the parent grid
            parent.Children.Add(verticalStack);

            // Create view
            dataView = new View();
            dataView.Initialize(viewParent, initialModel);
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var add = new Group(OnClickGroup)
            {
                Header = "Group " + (groupSelectorTree.Items.Count + 1),
            };
            add.IsSelected = true;
            groupSelectorTree.Items.Add(add);
            dataView.SetModel(add.Model);
        }

        private void OnClickGroup(object sender, MouseButtonEventArgs e)
        {
            if(sender is Group group)
            {
                dataView.SetModel(group.Model);
            }
        }

        private class Group : TreeViewItem
        { 
            public Model Model { get; private set; }

            public Group(MouseButtonEventHandler onClick) : this(new Model(), onClick) { }

            public Group(Model model, MouseButtonEventHandler onClick) : base()
            {
                PreviewMouseLeftButtonDown += onClick;
                Model = model;
            }
        }

    }
}
