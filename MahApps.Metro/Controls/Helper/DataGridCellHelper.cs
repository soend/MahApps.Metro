﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace MahApps.Metro.Controls
{
    public class DataGridCellHelper
    {
        public static readonly DependencyProperty SaveDataGridProperty =
            DependencyProperty.RegisterAttached("SaveDataGrid", typeof(bool), typeof(DataGridCellHelper),
                                                new FrameworkPropertyMetadata(default(bool),
                                                                              FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                                              CellPropertyChangedCallback));

        private static void CellPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var cell = dependencyObject as DataGridCell;
            if (cell != null && e.NewValue != e.OldValue && e.NewValue is bool)
            {
                cell.Unloaded -= DataGridCellUnloaded;
                var dataGrid = cell.TryFindParent<DataGrid>();
                if ((bool)e.NewValue)
                {
                    cell.Unloaded += DataGridCellUnloaded;
                    SetDataGrid(cell, dataGrid);
                }
                else
                {
                    SetDataGrid(cell, null);
                }
            }
        }

        static void DataGridCellUnloaded(object sender, RoutedEventArgs e)
        {
            SetDataGrid((DataGridCell)sender, null);
        }

        /// <summary>
        /// Save the DataGrid.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(DataGridCell))]
        public static bool GetSaveDataGrid(UIElement element)
        {
            return (bool)element.GetValue(SaveDataGridProperty);
        }

        public static void SetSaveDataGrid(UIElement element, bool value)
        {
            element.SetValue(SaveDataGridProperty, value);
        }

        public static readonly DependencyProperty DataGridProperty =
            DependencyProperty.RegisterAttached("DataGrid", typeof(DataGrid), typeof(DataGridCellHelper),
                                                new FrameworkPropertyMetadata(default(DataGrid), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Get the DataGrid.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(DataGridCell))]
        public static DataGrid GetDataGrid(UIElement element)
        {
            return (DataGrid)element.GetValue(DataGridProperty);
        }

        public static void SetDataGrid(UIElement element, DataGrid value)
        {
            element.SetValue(DataGridProperty, value);
        }
    }
}