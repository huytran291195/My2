using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

    public class Libralys
    {
        public static OdbcType Type(string name)
        {
            switch (name.ToLower())
            {
                case "bigint":
                    return OdbcType.BigInt;
                case "binary":
                    return OdbcType.Binary;
                case "bit":
                    return OdbcType.Bit;
                case "char":
                    return OdbcType.Char;
                case "date":
                    return OdbcType.Date;
                case "datetime":
                    return OdbcType.DateTime;
                case "decimal":
                    return OdbcType.Decimal;
                case "double":
                    return OdbcType.Double;
                case "image":
                    return OdbcType.Image;
                case "int":
                    return OdbcType.Int;
                case "nChar":
                    return OdbcType.NChar;
                case "nText":
                    return OdbcType.NText;
                case "numeric":
                    return OdbcType.Numeric;
                case "nvarchar":
                    return OdbcType.NVarChar;
                case "real":
                    return OdbcType.Real;
                case "smalldatetime":
                    return OdbcType.SmallDateTime;
                case "smallint":
                    return OdbcType.SmallInt;
                case "text":
                    return OdbcType.Text;
                case "time":
                    return OdbcType.Time;
                case "timestamp":
                    return OdbcType.Timestamp;
                case "tinyint":
                    return OdbcType.TinyInt;
                case "uniqueidentifier":
                    return OdbcType.UniqueIdentifier;
                case "varbinary":
                    return OdbcType.VarBinary;
                case "varchar":
                    return OdbcType.VarChar;
                case "money":
                    return OdbcType.Decimal;
                default:
                    return OdbcType.VarChar;
            }

        }
    }
