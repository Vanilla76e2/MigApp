using MigApp.MVVM.Model;
using MigApp.MVVM.Model.CRWindows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MigApp.Services
{
    internal class GetModel
    {
        PostgreSQLClass pgsql = PostgreSQLClass.GetInstance();
        public async Task<List<ComputersModel>> Computers(string filter)
        {
            var computers = new List<ComputersModel>();
            DataTable computersTable = await pgsql.GetTable("*","\"Technic\".computers_view",filter);
            foreach(DataRow row in computersTable.Rows)
            {
                DataTable componentsTable = await pgsql.GetTable("*", "\"Technic\".computers_components", $"Where computer_id = {row["id"].ToString()}");
                string Components = "";
                foreach(DataRow component_row in componentsTable.Rows)
                {
                    Components += $"{component_row["component_name"].ToString()}, {component_row["component_specifies"].ToString()}\n";
                }
                computers.Add(new ComputersModel
                {
                    id = row["id"].ToString(),
                    inventory_number = row["inventory_number"].ToString(),
                    name = row["name"].ToString(),
                    ip = row["ip"].ToString(),
                    fio = row["fio"].ToString(),
                    room = row["room"].ToString(),
                    components = Components,
                    operating_system = row["operating_system"].ToString(),
                    comment = row["comment"].ToString()
                });
            }
            return computers;
        }

        public async Task<List<ComputerComponentsModel>> ComputerComponents(string id)
        {
            var components = new List<ComputerComponentsModel>();
            DataTable componentsTable = await pgsql.GetTable("*","\"Technic\".computers_components", $"Where computer_id = {id}");
            foreach(DataRow row in componentsTable.Rows)
            {
                components.Add(new ComputerComponentsModel
                {
                    id = row["component_id"].ToString(),
                    name = row["component_name"].ToString(),
                    inventory_number = row["component_invnum"].ToString(),
                    specifies = row["component_specifies"].ToString()
                });
            }
            return components;
        }

        public async Task<List<PersonComboBoxModel>> UsersFIO()
        {
            var fioList = new List<PersonComboBoxModel>();
            DataTable dataTable = await pgsql.GetTable("id, fio", "\"Employees\".employees", "Where deleted = false");
            foreach (DataRow row in dataTable.Rows)
            {
                fioList.Add(new PersonComboBoxModel
                {
                    id = Convert.ToInt32(row["id"]),
                    fio = row["fio"].ToString()
                });
            }
            return fioList;
        }
    }
}
