using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ConsultaDapperSQL.Configuracao;
using ConsultaDapperSQL.Modelo;

namespace ConsultaDapperSQL.Repositorio
{
    public class AeroportoRepositoryCD : IAeroportoRepositoryCD
    {
        private string _configuracao;
        public AeroportoRepositoryCD()
        {
            _configuracao = DataBaseConfiguration.Get();
        }

        //static string connString = @"Data Source= " + dataSource + ";Initial Catalog="
        //    + database + ";Persist Security Info = True;User ID=" + username + ";Password=" + password;

        //SqlConnection connection = new SqlConnection(connString);

        public bool Add(AeroportoCD aeroporto)
        {
            bool status = false;

            using (var db = new SqlConnection(_configuracao))
            {
                //db.Close();

                db.Open();
                db.Execute(AeroportoCD.INSERT, aeroporto);
                status = true;
                db.Close();
            }

            return status;
        }

        public List<AeroportoCD> GetAll()
        {
            using (var db = new SqlConnection(_configuracao))
            {
                db.Open();
                var aeroportos = db.Query<AeroportoCD>(AeroportoCD.GETALL);

                return (List<AeroportoCD>)aeroportos;
            }
        }

        public AeroportoCD GetId(int id)
        {
            using (var db = new SqlConnection(_configuracao))
            {
                try
                {
                    db.Open();
                    var aeroporto = db.QueryFirst<AeroportoCD>(AeroportoCD.GETID, new { Id = id });
                    return (AeroportoCD)aeroporto;
                }
                catch
                {

                    return null;
                }
            }
        }
    }
}
