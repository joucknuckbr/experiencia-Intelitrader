using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Console
{
    class Conexão
    {
        private MySqlConnection mConn;
        private MySqlConnection mConn2;

        public Conexão()
        { 
            //define string de conexao e cria a conexao

            string MyConnectionString = "Persist Security Info=False;Server=localhost;Port=3306;Database=origem;Uid=root;Password=19420516";
            mConn = new MySqlConnection(MyConnectionString);
            string MyConnectionString2 = "Persist Security Info=False;Server=localhost;Port=3307;Database=destino;Uid=jouck;Password=19420516";
            mConn2 = new MySqlConnection(MyConnectionString2);

            mConn.Open();

            if (mConn.State == ConnectionState.Open)
            {

                string sql = "SELECT usuario.id, nome, data_nasc, endereco, complemento, cidade, estado FROM usuario INNER JOIN endereco ON usuario.id_endereco = endereco.id; ";
                string sql2 = "SELECT biblioteca.id, nomeJ, ano, data_aquisicao, nomeG, descricao, nomeU FROM biblioteca INNER JOIN( SELECT jogo.id, jogo.nome as nomeJ, ano, genero.nome as nomeG, descricao FROM jogo INNER JOIN genero ON jogo.id_genero = genero.id ) as jogo ON biblioteca.id_jogo = jogo.id INNER JOIN( SELECT usuario.id, nome as nomeU FROM usuario ) as usuario ON biblioteca.id_usuario = usuario.id; ";
                string delete = "DELETE FROM user;";
                string delete2 = "DELETE FROM library;";

                mConn.Close();
                mConn2.Open();

                MySqlCommand cmd = new MySqlCommand(delete, mConn2);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                }

                rdr.Close();
                cmd = new MySqlCommand(delete2, mConn2);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                }

                mConn.Open();
                mConn2.Close();
                rdr.Close();

                cmd = new MySqlCommand(sql, mConn);
                rdr = cmd.ExecuteReader();

                int i = 0;
                int j = 0;

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " - " + rdr[1] + " - " + rdr[2] + " - " + rdr[3] + " - " + rdr[4] + " - " + rdr[5] + " - " + rdr[6]);
                    i++;
                }


                rdr.Close();
                rdr = cmd.ExecuteReader();
                string[,] reader = new string[i,7];
                i = 0;

                while (rdr.Read())
                {
                    reader[i, 0] = rdr[0].ToString();
                    reader[i, 1] = rdr[1].ToString();
                    reader[i, 2] = rdr[2].ToString();
                    reader[i, 3] = rdr[3].ToString();
                    reader[i, 4] = rdr[4].ToString();
                    reader[i, 5] = rdr[5].ToString();
                    reader[i, 6] = rdr[6].ToString();               
                    i++;
                }
                
                rdr.Close();
                cmd = new MySqlCommand(sql2, mConn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " - " + rdr[1] + " - " + rdr[2] + " - " + rdr[3] + " - " + rdr[4] + " - " + rdr[5] + " - " + rdr[6]);
                    j++;
                }

                rdr.Close();
                rdr = cmd.ExecuteReader();

                string[,] reader2 = new string[j, 7];
                j = 0;
                DateTime local;
                String ts;

                while (rdr.Read())
                {
                    reader2[j, 0] = rdr[0].ToString();
                    reader2[j, 1] = rdr[1].ToString();
                    reader2[j, 2] = rdr[2].ToString();
                    local = DateTime.Parse(rdr[3].ToString());
                    ts = local.Year.ToString();
                    ts += "-" + local.Month + "-" + local.Day + " " + "0" + local.Hour + ":" + "0" + local.Minute + ":" + "0" + local.Second + ".0000";
                    reader2[j, 3] = rdr[3].ToString();
                    reader2[j, 4] = rdr[4].ToString();
                    reader2[j, 5] = rdr[5].ToString();
                    reader2[j, 6] = rdr[6].ToString();
                    j++;
                }


                var cont = i;
                string[] Query = new string[i];
                string[] Query2 = new string[j];
                i = 0;

                mConn.Close();
                mConn2.Open();
                rdr.Close();
                MySqlDataReader rdr2;
                MySqlCommand MyCommand2;

                string wait = Console.ReadLine();

                while (cont!=0)
                {
                    Query[i] = "insert into user(user.id, name, birthday, address, complement, city, state) values('" + reader[i,0] + "' , '" + reader[i,1] + "' , '" + reader[i,2] + "' , '" + reader[i,3] + "' , '" + reader[i,4] + "' , '" + reader[i,5] + "' , '" + reader[i,6] + "');";
                    Console.WriteLine(Query[i]);
                    MyCommand2 = new MySqlCommand(Query[i], mConn2);
                    rdr2 = MyCommand2.ExecuteReader();
                    while (rdr2.Read())
                    {
                    }
                    rdr2.Close();
                    i++;
                    cont--;
                }

                cont = j;
                j = 0;

                while (cont != 0)
                {
                    Console.WriteLine(reader2[j,0] + " - " + reader2[j, 1] + " - " + reader2[j, 2] + " - " + reader2[j, 3] + " - " + reader2[j, 4] + " - " + reader2[j, 5] + " - " + reader2[j, 6]);
                    Query2[j] = "insert into library(id, name, year, acquisition_date, genre, description, user) values('" + reader2[j, 0] + "' , '" + reader2[j, 1] + "' , '" + reader2[j, 2] + "' , '" + reader2[j, 3] + "' , '" + reader2[j, 4] + "' , '" + reader2[j, 5] + "' , '" + reader2[j, 6] + "')";
                    Console.WriteLine(Query2[j]);
                    MyCommand2 = new MySqlCommand(Query2[j], mConn2);
                    rdr2 = MyCommand2.ExecuteReader();
                    while (rdr2.Read())
                    {
                    }
                    rdr2.Close();
                    j++;
                    cont--;
                }

                mConn2.Close();

                //atribui a resultado a propriedade DataSource do DataGrid

            }
        }

    }
}
