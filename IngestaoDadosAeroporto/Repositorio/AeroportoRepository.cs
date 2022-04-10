using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IngestaoDadosAeroporto.Configuracao;
using IngestaoDadosAeroporto.Modelo;

namespace IngestaoDadosAeroporto.Repositorio
{
    public class AeroportoRepository : IAeroportoRepository
    {
        private string _configuracao;
        public AeroportoRepository()
        {
            _configuracao = DataBaseConfiguration.Get();
        }

        //static string connString = @"Data Source= " + dataSource + ";Initial Catalog="
        //    + database + ";Persist Security Info = True;User ID=" + username + ";Password=" + password;

        //SqlConnection connection = new SqlConnection(connString);

        public bool Add(Aeroporto aeroporto)
        {
            bool status = false;

            using (var db = new SqlConnection(_configuracao))
            {
                //db.Close();

                db.Open();
                db.Execute(Aeroporto.INSERT, aeroporto);
                status = true;
                db.Close();
            }

            return status;
        }

        public List<Aeroporto> GetAll()
        {
            using (var db = new SqlConnection(_configuracao))
            {
                db.Open();
                var aeroportos = db.Query<Aeroporto>(Aeroporto.GETALL);

                return (List<Aeroporto>)aeroportos;
            }
        }

        public Aeroporto GetId(int id)
        {
            using (var db = new SqlConnection(_configuracao))
            {
                try
                {
                    db.Open();
                    var aeroporto = db.QueryFirst<Aeroporto>(Aeroporto.GETID, new { id });
                    return (Aeroporto)aeroporto;
                }
                catch
                {
                    return null;
                }
            }

        }

    }
}
