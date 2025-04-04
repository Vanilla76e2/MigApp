﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data;
using MigApp.Core;

namespace MigApp.MVVM.ViewModel
{
    internal class TabletsViewModel : Core.ViewModel
    {
        

        #region Таблица

        private DataTable table;
        public DataTable Table
        {
            get => table;
            set
            {
                table = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadTableAsync()
        {
            //try
            //{
            //    DataTable temp = await pgsql.GetTable("id, inventory_number, model, fio, room, serial_number, diagonal, operating_system, processor, ram, drive, other, comment", "\"Technic\".tablets_view", Filter);
            //    temp.Columns["id"].ColumnName = "ID";
            //    temp.Columns["inventory_number"].ColumnName = "Инвентарный номер";
            //    temp.Columns["model"].ColumnName = "Модель";
            //    temp.Columns["serial_number"].ColumnName = "Серийный номер";
            //    temp.Columns["fio"].ColumnName = "Пользователь";
            //    temp.Columns["room"].ColumnName = "Кабинет";
            //    temp.Columns["diagonal"].ColumnName = "Диагональ";
            //    temp.Columns["operating_system"].ColumnName = "ОС";
            //    temp.Columns["processor"].ColumnName = "Процессор";
            //    temp.Columns["ram"].ColumnName = "ОЗУ";
            //    temp.Columns["drive"].ColumnName = "Накопители";
            //    temp.Columns["other"].ColumnName = "Прочие комплектующие";
            //    temp.Columns["comment"].ColumnName = "Комментарий";
            //    Table = temp;
            //}
            //catch
            //{
            //    MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        #endregion

        #region Фильтр

        private string filter = "Where deleted = False";
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
#pragma warning disable CS4014
                LoadTableAsync();
#pragma warning restore CS4014
            }
        }

        public void ClearFilter()
        {
            Filter = "Where deleted = False";
        }

        public void ApplyFilter()
        {

        }

        #endregion

        #region Команды
        // Очистить филтр
        public ICommand ClearFilterCommand { get; }

        // Очистить филтр
        public ICommand ApplyFilterCommand { get; }

        // Добавить запись
        public ICommand AddCommand { get; }

        // Редактирование
        public ICommand RedactCommand { get; }

        // Копирование
        public ICommand MyCopyCommand { get; }

        // Удаление
        public ICommand MyDeleteCommand { get; }

        // Отчёт
        public ICommand ReportCommand { get; }

        private void AddEmployee()
        {

        }

        private void Redact()
        {

        }

        private void MyCopy()
        {

        }

        private void MyDelete()
        {

        }

        private void Report()
        {

        }
        #endregion

        public TabletsViewModel()
        {
            AddCommand = new RelayCommand(o => AddEmployee(), o => true);

            ClearFilterCommand = new RelayCommand(o => ClearFilter(), o => { return Filter != "WHERE deleted = False"; });
            ApplyFilterCommand = new RelayCommand(o => ApplyFilter(), o => true);

            MyCopyCommand = new RelayCommand(o => MyCopy(), o => true);
            RedactCommand = new RelayCommand(o => Redact(), o => true);
            MyDeleteCommand = new RelayCommand(o => MyDelete(), o => true);
            ReportCommand = new RelayCommand(o => Report(), o => true);

        }
    }
}
