using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Serializacion
    {
        public void SerializarXML(string path, DataGridView dgv)
        {
            DataTable dt = ConvertirGrillaEnDataTable(dgv);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                serializer.Serialize(fs, dt);
            }
        }

        public DataTable DeserializarXML(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                return (DataTable)serializer.Deserialize(fs);
            }
        }

        public DataTable ConvertirGrillaEnDataTable(DataGridView dgv)
        {
            DataTable dt = new DataTable("Clientes");

            foreach (DataGridViewColumn c in dgv.Columns)
            {
                dt.Columns.Add(c.Name, c.ValueType);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dr[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
