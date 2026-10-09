using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyCongTyDuLich.Services;
namespace QuanLyCongTyDuLich.Data
{
    // SQL identifiers come EXCLUSIVELY from CatalogService.Entities, never directly from user input.
    public sealed class CatalogRepository
    {
        public DataTable List(CatalogSpec spec)
        { return Db.Query("SELECT * FROM dbo.[" + spec.TableName + "] ORDER BY [" + spec.Fields[0].Name + "]"); }
        public void Save(CatalogSpec spec, IDictionary<string, object> values, bool update)
        {
            var cols = spec.Fields.Select(f => "[" + f.Name + "]").ToArray();
            var pars = spec.Fields.Select((f, i) => "@p" + i).ToArray();
            string sql;
            if (update)
                sql = "UPDATE dbo.[" + spec.TableName + "] SET " + string.Join(",", cols.Skip(1).Select((c, i) => c + "=" + pars[i + 1])) + " WHERE " + cols[0] + "=@p0";
            else
                sql = "INSERT INTO dbo.[" + spec.TableName + "] (" + string.Join(",", cols) + ") VALUES (" + string.Join(",", pars) + ")";
            var args = spec.Fields.Select((f, i) => Db.P("@p" + i, values[f.Name])).ToArray();
            if (Db.Execute(sql, args) == 0) throw new InvalidOperationException("Khong tim thay ma de cap nhat.");
        }
        public void Delete(CatalogSpec spec, string key)
        {
            var sql = "DELETE FROM dbo.[" + spec.TableName + "] WHERE [" + spec.Fields[0].Name + "]=@id";
            if (Db.Execute(sql, Db.P("@id", key)) == 0) throw new InvalidOperationException("Khong tim thay ban ghi can xoa.");
        }
    }
}
