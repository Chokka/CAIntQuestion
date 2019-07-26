using System.Linq;

using System.Data;
using System.IO;




namespace CourtAlertIntQuestion
{
    class Program
    {
        static void Main(string[] args)
        {
            PopulateData();
        }


        public static void PopulateData()
        {

            //DataTable dt = new DataTable();
            //dt = ConvertCSVtoDataTable(@"C:\Users\ChokkaXamarin\Downloads\Interview\restaurant-ratings.csv");


            //Prepare Datatable and Add All Columns Here
            DataTable dt = new DataTable();
            //DataRow row;
            DataColumn dc = new DataColumn();
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ID";
            dc.ReadOnly = false;
            dc.Unique = false;
            dc.AutoIncrement = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "City";
            dc.ReadOnly = false;
            dc.Unique = false;
            dc.AutoIncrement = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Cuisine";
            dc.ReadOnly = false;
            dc.Unique = false;
            dc.AutoIncrement = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "Rating";
            dc.ReadOnly = false;
            dc.Unique = false;
            dc.AutoIncrement = false;
            dt.Columns.Add(dc);


            dt = ConvertCSVtoDataTable(@"C:\Users\ChokkaXamarin\Downloads\Interview\restaurant-ratings.csv");
            DataTable Dt2 = new DataTable();

            Dt2 = dt.AsEnumerable()
              .GroupBy(r => r.Field<string>("City"))
              .Select(g => 
              {
                  var row = dt.NewRow();
                  row["City"] = g.Key;
                  row["rating"] = g.Average(r => ParseInt32(r.Field<string>("rating")));
                  return row;
              })
              .OrderByDescending(row => row["rating"])
              .CopyToDataTable();



            //try
            //{
            //    var results = from r in Dt2.DataSet.Tables[0].AsEnumerable()                              
            //                  .Select (r => new
            //                  {
            //                      ProductName = r.Field<string>("City"),
            //                      Description = r.Field<int>("rating")
            //                  });
            //}
            //catch (System.Exception ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //}


        }



        public static int ParseInt32(string str, int defaultValue = 0)
        {
            int result;
            return System.Int32.TryParse(str, out result) ? result : defaultValue;
        }


        //public static int? ParseInt32(string str)
        //{
        //    int result;
        //    return System.Int32.TryParse(str, out result) ? result : null;
        //}

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }

    }
}
