using System;
using System.Data;

using NQuery;

namespace Northwind
{
    public class NorthwindDb
    {
        public static string GetData()
        {
            var result = "";
            var dataSet = new DataSet();
            dataSet.ReadXml(@"P:\fringe\northwind.xml");

            var dataContext = new DataContext();
            dataContext.AddTablesAndRelations(dataSet);

            var sql = @"
                SELECT  FirstName + ' ' + LastName Name,
                        Birthdate.AddYears(65) RetirementDate
                FROM    Employees
                WHERE   Birthdate.AddYears(65) < GETDATE()
            ";

            var query = new Query(sql, dataContext);
            var resultsTable = query.ExecuteDataTable();

            foreach (DataRow row in resultsTable.Rows)
            {
                var name = Convert.ToString(row["Name"]);
                var retirementDate = Convert.ToDateTime(row["RetirementDate"]); ;

                result += $"{name}: {retirementDate}\r\n";
            }

            return result;
        }
    }
}
