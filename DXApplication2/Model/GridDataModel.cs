using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.DataAccess.Sql;
using System.Data;
using System.Data.SQLite;
using DevExpress.DataAccess.Native.Sql;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace DXApplication2.Model
{
    public class GridDataModel
    {
        private SqlDataSource dataSource;
        private DataTable loadedTable;
        public DataTable LoadedTable
        {
            get { return loadedTable; }
        }
        private int lastTableRowIndex;
        public GridDataModel()
        {
            loadedTable = new DataTable();
        }
        public void GetDataTableBySource(SqlDataSource source)
        {
            dataSource = source;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Main");
            ResultSet rs = (dataSource as IListSource).GetList() as ResultSet;
            ResultTable rt = rs.Tables.First();
            List<string> columnList = new List<string>();

            dt.Clear();

            if (rt != null)
            {
                //Add columns  
                foreach (ResultColumn rc in rt.Columns)
                {
                    DataColumn dc = new DataColumn(rc.DisplayName);
                    dc.DataType = rc.PropertyType;
                    dt.Columns.Add(rc.DisplayName);
                    columnList.Add(rc.DisplayName);
                }

                //Add rows  
                for (int i = 0; i < rt.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    ArrayList rowVals = new ArrayList();

                    foreach (ResultColumn rc in rt.Columns)
                    {
                        rowVals.Add(rc.GetValue((ResultRow)((IList)rt)[i]));
                    }

                    dr.ItemArray = rowVals.ToArray();
                    dt.Rows.Add(dr);
                    dr.AcceptChanges();
                }

                loadedTable = dt;
                lastTableRowIndex = loadedTable.Rows.Count - 1;
            }
        }
        public void AddRow()
        {
            loadedTable.Rows.Add(loadedTable.NewRow());
        }
        public void DeleteRow()
        {
            if (loadedTable.Rows.Count > 0)
            {
                loadedTable.Rows[lastTableRowIndex].Delete();
                lastTableRowIndex -= 1;
            }
        }
        public void RollbackChanges()
        {
            loadedTable.RejectChanges();
        }
        public void UpdateDataChanged()
        {
            try
            {
                string connectSTR = @"Data Source=C:\Users\Administrator\Desktop\PracticeApp\test.db";
                using (SQLiteConnection conn = new SQLiteConnection(connectSTR))
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(conn);

                    // 데이터 Delete 반영
                    Debug.Write(loadedTable.Rows.Count);
                    DataTable deletedTable = loadedTable.GetChanges(DataRowState.Deleted);
                   
                    if (deletedTable != null)
                    {
                        StringBuilder builder = new StringBuilder($"DELETE FROM Main WHERE {deletedTable.Columns[0].ColumnName} = ");
                        foreach (DataRow row in deletedTable.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted)
                            {

                            }
                        }
                    }
                    // 데이터 Insert 반영
                    DataTable addedTable = loadedTable.GetChanges(DataRowState.Added);
                    if (addedTable != null)
                    {
                        StringBuilder builder = new StringBuilder("INSERT INTO Main VALUES (");
                        foreach (DataRow row in addedTable.Rows)
                        {
                            if (row.RowState == DataRowState.Added)
                            {
                                object[] items = row.ItemArray;
                                for (int i = 0; i < items.Length; ++i)
                                {
                                    if (i == items.Length - 1)
                                        builder.Append($"{items[i]});");
                                    else
                                        builder.Append($"{items[i]}, ");
                                }
                            }
                        }
                        cmd.CommandText = builder.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"저장 실패 : {e.Message}");
                return;
            }
            MessageBox.Show("저장 성공");
        }
    }
}
