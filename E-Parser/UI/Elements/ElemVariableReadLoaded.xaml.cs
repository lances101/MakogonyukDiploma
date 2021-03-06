﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using E_Parser.Logic;
using E_Parser.Logic.ElementLogic;

namespace E_Parser.UI.Elements
{
    /// <summary>
    /// Interaction logic for ElemTextInput.xaml
    /// </summary>
    public partial class ElemVariableReadLoaded : ElemBase
    {
        public ElemVariableReadLoaded()
        {
            InitializeComponent();
        }
        public ElemVariableReadLoaded(TaskSession ts)
        {
            InitializeComponent();
            Task = new TSVariableReadLoaded(ts);
        }
        public ElemVariableReadLoaded(TSBase ts)
        {
            InitializeComponent();
            Task = ts;
            
        }

        public override void AfterElementAddition()
        {
            var ind = Task.Session.SaveableVariables.FindIndex(o => o.Name == SelectedStoredVariable.Name);
            this.cmbbxVarRead.SelectedIndex = ind;
            this.cmbbxVarRead.Items.Refresh();
        }

        public List<StoredVariable> SessionProxy
        {
            get { return Task.Session.SaveableVariables; }
            set { Task.Session.SaveableVariables = value; }
        }

        public StoredVariable SelectedStoredVariable
        {
            get
            {
                return (Task as TSVariableReadLoaded).StoredVariable;
            }
            set { (Task as TSVariableReadLoaded).StoredVariable = value; }
        }

        private void CmbbxVarRead_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                SelectedStoredVariable = e.RemovedItems[0] as StoredVariable;
                cmbbxVarRead.SelectedItem = e.RemovedItems[0];
            }
            else if(this.SelectedStoredVariable.Name == null)
            {
                SelectedStoredVariable = e.AddedItems[0] as StoredVariable;
            }

        }
    }
}
