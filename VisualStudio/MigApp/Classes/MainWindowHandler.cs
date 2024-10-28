using MigApp.Classes;
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MigApp.Classes
{
    public class MainWindowHandler : INotifyPropertyChanged
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();
        MiscClass mc = new MiscClass();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Window Setup
        public string WindowTitle { get; set; } = "MigApp v." + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        public string CurrentUser { get; set; } = MigApp.Properties.Settings.Default.UserLogin;
        public double WindowMaxHeigt { get; set; } = System.Windows.SystemParameters.PrimaryScreenHeight - 50;
        public double WindowMaxWidth { get; set; } = System.Windows.SystemParameters.PrimaryScreenWidth + 6;
        #endregion

        #region Tables
        #region Users Tables
        private DataTable favouriteTable { get; set; }
        public DataTable FavouriteTable
        {
            get => favouriteTable;
            set { favouriteTable = value; OnPropertyChanged(); }
        }
        private DataTable employeesTable { get; set; }
        public DataTable EmployeesTable
        {
            get => employeesTable;
            set {  employeesTable = value; OnPropertyChanged(); }
        }
        private DataTable employeesGroupTable { get; set; }
        public DataTable EmployeesGroupTable
        {
            get => employeesGroupTable;
            set { employeesGroupTable = value; OnPropertyChanged(); }
        }
        private DataTable computersTable { get; set; }
        public DataTable ComputersTable
        {
            get => computersTable;
            set { computersTable = value; OnPropertyChanged(); }
        }
        private DataTable tabletsTable { get; set; }
        public DataTable TabletsTable
        {
            get => tabletsTable;
            set { tabletsTable = value; OnPropertyChanged(); }
        }
        private DataTable laptopsTable { get; set; }
        public DataTable LaptopsTable
        {
            get => laptopsTable;
            set { laptopsTable = value; OnPropertyChanged(); }
        }
        private DataTable orgtechTable { get; set; }
        public DataTable OrgtechTable
        {
            get => orgtechTable;
            set { orgtechTable = value; OnPropertyChanged(); }
        }
        private DataTable monitorsTable { get; set; }
        public DataTable MonitorsTable
        {
            get => monitorsTable;
            set { monitorsTable = value; OnPropertyChanged(); }
        }
        private DataTable routersTable { get; set; }
        public DataTable RoutersTable
        {
            get => routersTable;
            set { routersTable = value; OnPropertyChanged(); }
        }
        private DataTable switchesTable { get; set; }
        public DataTable SwitchesTable
        {
            get => switchesTable;
            set { switchesTable = value; OnPropertyChanged(); }
        }
        private DataTable cctvTable { get; set; }
        public DataTable CCTVTable
        {
            get => cctvTable;
            set { cctvTable = value; OnPropertyChanged(); }
        }
        private DataTable furnitureTable { get; set; }
        public DataTable FurnitureTable
        {
            get => furnitureTable;
            set { furnitureTable = value; OnPropertyChanged(); }
        }
        private DataTable furnitureTypeTable { get; set; }
        public DataTable FurnitureTypeTable
        {
            get => FurnitureTypeTable;
            set { FurnitureTypeTable = value; OnPropertyChanged(); }
        }
        #endregion

        #region Admins Tables
        private DataTable usersTable { get; set; } = null;
        public DataTable UsersTable
        {
            get => usersTable;
            set { usersTable = value; OnPropertyChanged(); }
        }
        private DataTable rolesTable { get; set; } = null;
        public DataTable RolesTable
        {
            get => rolesTable;
            set { rolesTable = value; OnPropertyChanged(); }

        }
        private DataTable logsTable { get; set; } = null;
        public DataTable LogsTable
        {
            get => logsTable;
            set { logsTable = value; OnPropertyChanged(); }
        }
        private DataTable ipTable { get; set; } = null;
        public DataTable IPTable
        {
            get => ipTable;
            set { ipTable = value; OnPropertyChanged(); }
        }
        #endregion

        #region Archive
        private DataTable employeesRemoved { get; set; } = null;
        public DataTable EmployeesRemoved
        {
            get => employeesRemoved;
            set { employeesRemoved = value; OnPropertyChanged(); }
        }
        private DataTable computersRemoved { get; set; } = null;
        public DataTable ComputersRemoved
        {
            get => computersRemoved;
            set { computersRemoved = value; OnPropertyChanged(); }
        }
        private DataTable tabletsRemoved { get; set; } = null;
        public DataTable TabletsRemoved
        {
            get => tabletsRemoved;
            set { tabletsRemoved = value; OnPropertyChanged(); }
        }
        private DataTable notebooksRemoved { get; set; } = null;
        public DataTable NotebooksRemoved
        {
            get => notebooksRemoved;
            set { notebooksRemoved = value; OnPropertyChanged(); }
        }
        private DataTable orgtechRemoved { get; set; } = null;
        public DataTable OrgtechRemoved
        {
            get => orgtechRemoved;
            set { orgtechRemoved = value; OnPropertyChanged(); }
        }
        private DataTable monitorsRemoved { get; set; } = null;
        public DataTable MonitorsRemoved
        {
            get => monitorsRemoved;
            set { monitorsRemoved = value; OnPropertyChanged(); }
        }
        private DataTable routersRemoved { get; set; } = null;
        public DataTable RoutersRemoved
        {
            get => routersRemoved;
            set { routersRemoved = value; OnPropertyChanged(); }
        }
        private DataTable switchesRemoved { get; set; } = null;
        public DataTable SwitchesRemoved
        {
            get => switchesRemoved;
            set { switchesRemoved = value; OnPropertyChanged(); }
        }
        private DataTable cctvRemoved { get; set; } = null;
        public DataTable CCTVRemoved
        {
            get => cctvRemoved;
            set { cctvRemoved = value; OnPropertyChanged(); }
        }
        public DataTable furnitureRemoved { get; set; } = null;
        public DataTable FurnitureRemoved
        {
            get => furnitureRemoved;
            set { furnitureRemoved = value; OnPropertyChanged(); }
        }
        #endregion
        #endregion

        public Object SelectedBorder { get; set; }

    }
}
