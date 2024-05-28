using ExcelDataReader;
using System.Data;

namespace Api.Lib
{
    public class TExcelReader
    {
        public TExcelReader()
        {

        }

      
        //public DataTable GenerateExcelDataTable(string FileName, Stream FileContent)
        //{
        //    DataTable result = new DataTable();
        //    System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        //    try
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        //        IExcelDataReader excelReader = null;

        //        ExcelDataSetConfiguration x_config = new ExcelDataSetConfiguration();
        //        x_config.ConfigureDataTable = tableReader => new ExcelDataTableConfiguration()
        //        {
        //            UseHeaderRow = true
        //        };
        //        x_config.UseColumnDataType = true;

        //        string path = System.IO.Path.GetFullPath(HttpContext.Current.Server.MapPath(FileName));
        //        if (Path.GetExtension(path) == ".xls")
        //        {
        //            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
        //            excelReader = ExcelReaderFactory.CreateBinaryReader(FileContent);
        //        }
        //        else if (Path.GetExtension(path) == ".xlsx")
        //        {
        //            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        //            excelReader = ExcelReaderFactory.CreateOpenXmlReader(FileContent);
        //        }
        //        DataSet dresult = excelReader.AsDataSet(x_config);
        //        DataTable tbupload = dresult.Tables[0];
        //        excelReader.Close();
        //        result = tbupload;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        //    }
        //    return result;
        //}
        public DataTable GenerateExcelDataTable(string SheetName, string FileName, Stream FileContent)
        {
            DataTable result = new DataTable();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                IExcelDataReader excelReader = null;

                ExcelDataSetConfiguration x_config = new ExcelDataSetConfiguration();
                x_config.ConfigureDataTable = tableReader => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                };
                x_config.UseColumnDataType = true;
                //conf.FallbackEncoding = Encoding.GetEncoding(1252)
                //string path = System.IO.Path.GetFullPath(HttpContext.Current.Server.MapPath(FileName));
                if (Path.GetExtension(FileName) == ".xls")
                {
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(FileContent);
                }
                else if (Path.GetExtension(FileName) == ".xlsx")
                {
                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(FileContent);
                }
                DataSet dresult = excelReader.AsDataSet(x_config);

                if (!dresult.Tables.Contains(SheetName))
                    return null;

                DataTable tbupload = dresult.Tables[SheetName];
                excelReader.Close();
                result = tbupload;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
            }
            return result;
        }

    }
}
