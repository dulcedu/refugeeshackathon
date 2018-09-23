using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class sqlfunciones
    {
        public string connectionString = "Data Source=mydbamazonqr.cws3a572kwo6.us-east-1.rds.amazonaws.com,1433;" +
            "Initial Catalog=dbRefugiados;Persist Security Info=False;User" +
            " ID=qrAmazon2018;Password=Danonino2018;";

        public string reg_usuario;
        public string  RetValUser()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                string cadena = "select habilitado from HabilitarBiometrico ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    reg_usuario = registros["habilitado"].ToString();
                }
                conexion.Close();
                return reg_usuario;
            }
            catch (Exception erc)
            {
                return "0";
            }
        }

        public string RetValUserTipo()
        {
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                string cadena = "select Habilitada from IdFingerprint ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    reg_usuario = registros["Habilitada"].ToString();
                }
                conexion.Close();
                return reg_usuario;
            }
            catch (Exception erc)
            {
                return "0";
            }
        }


        public Boolean HabilitarBiometricoSI()
        {
            Boolean ver_act_dep;
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                string cadena = "update HabilitarBiometrico set habilitado='SI' ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                //SqlDataReader registros = comando.ExecuteReader();
                //mtv.LOG_MTV_SQL(cadena);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    ver_act_dep = true;
                }
                else
                {
                    ver_act_dep = false;
                }
                conexion.Close();
                return ver_act_dep;
            }
            catch (Exception erc)
            {
                return false;
            }
        }


        public Boolean HabilitarBiometricoNO()
        {
            Boolean ver_act_dep;
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                string cadena = "update HabilitarBiometrico set habilitado='NO";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                //SqlDataReader registros = comando.ExecuteReader();
                //mtv.LOG_MTV_SQL(cadena);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    ver_act_dep = true;
                }
                else
                {
                    ver_act_dep = false;
                }
                conexion.Close();
                return ver_act_dep;
            }
            catch (Exception erc)
            {
                return false;
            }
        }

        public Boolean RegTiendaSI()
        {
            Boolean ver_act_dep;
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                string cadena = "update IdFingerprint set Habilitada='SI' ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                //SqlDataReader registros = comando.ExecuteReader();
                //mtv.LOG_MTV_SQL(cadena);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    ver_act_dep = true;
                }
                else
                {
                    ver_act_dep = false;
                }
                conexion.Close();
                return ver_act_dep;
            }
            catch (Exception erc)
            {          
                return false;
            }
        }

        public Boolean RegTiendaNO()
        {
            Boolean ver_act_dep;
            try
            {
                SqlConnection conexion = new SqlConnection(connectionString);
                conexion.Open();
                string cadena = "update IdFingerprint set Habilitada='NO' ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                //SqlDataReader registros = comando.ExecuteReader();
                //mtv.LOG_MTV_SQL(cadena);
                int cant;
                cant = comando.ExecuteNonQuery();
                if (cant == 1)
                {
                    ver_act_dep = true;
                }
                else
                {
                    ver_act_dep = false;
                }
                conexion.Close();
                return ver_act_dep;
            }
            catch (Exception erc)
            {
                //String error_update = "update watermarker set activo='SI' where id_edo=" + id_edo + "- Exception" + erc.Message;
                //Error_SQL(error_update);
                //EscribirLogErrorSQL(error_update);
                //mtv.LOG_MTV_SQL(error_update);
                return false;
            }
        }
    }
}
