using MigApp.Classes;
using System.Data;
using System.Reflection;

namespace MigApp.Classes
{
    class MainWindowHandler
    {
        SQLConnectionClass sqlcc = SQLConnectionClass.getinstance();

        #region Window Setup
        public string WindowTitle { get; set; } = "MigApp v." + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        public string CurrentUser { get; set; } = MigApp.Properties.Settings.Default.UserLogin;
        public double WindowMaxHeigt { get; set; } = System.Windows.SystemParameters.PrimaryScreenHeight - 50;
        public double WindowMaxWidth { get; set; } = System.Windows.SystemParameters.PrimaryScreenWidth + 6;
        #endregion

        #region Tables
        #region Users Tables
        public DataTable FavouriteTable { get; set; } = null;
        public DataTable EmployeesTable { get; set; } = null;
        public DataTable EmployeesGroupTable { get; set; } = null;
        public DataTable ComputersTable { get; set; } = null;
        public DataTable TabletsTable { get; set; } = null;
        public DataTable NotebooksTable { get; set; } = null;
        public DataTable OrgtechTable { get; set; } = null;
        public DataTable MonitorsTable { get; set; } = null;
        public DataTable RoutersTable { get; set; } = null;
        public DataTable SwitchesTable { get; set; } = null;
        public DataTable CCTVTable { get; set; } = null;
        public DataTable FurnitureTable { get; set; } = null;
        public DataTable FurnitureTypeTable { get; set; } = null;
        #endregion

        #region Admins Tables
        public DataTable UsersTable { get; set; } = null;
        public DataTable RolesTable { get; set; } = null;
        public DataTable LogsTable { get; set; } = null;
        public DataTable IPTable { get; set; } = null;
        #endregion

        #region Archive
        public DataTable EmployeesRemoved { get; set; } = null;
        public DataTable ComputersRemoved { get; set; } = null;
        public DataTable TabletsRemoved { get; set; } = null;
        public DataTable NotebooksRemoved { get; set; } = null;
        public DataTable OrgtechRemoved { get; set; } = null;
        public DataTable MonitorsRemoved { get; set; } = null;
        public DataTable RoutersRemoved { get; set; } = null;
        public DataTable SwitchesRemoved { get; set; } = null;
        public DataTable CCTVRemoved { get; set; } = null;
        public DataTable FurnitureRemoved { get; set; } = null;
        #endregion
        #endregion
    }
}
