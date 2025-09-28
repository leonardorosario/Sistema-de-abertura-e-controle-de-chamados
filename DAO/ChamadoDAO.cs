using CadAlunoMVC.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace CadAlunoMVC.DAO
{
    public class ChamadoDAO
    {
        public void Inserir(ChamadoViewModel chamado)
        {
            string sql =
            "insert into chamados(id, dataAbertura, descricaoProblema, descricaoAtendimento, dataAtendimento, situacao, usuarioId)" +
            "values ( @id, @dataAbertura, @descricaoProblema, @descricaoAtendimento, @dataAtendimento, @situacao, @usuarioId)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(chamado));
        }
        public void Alterar(ChamadoViewModel chamado)
        {
            string sql =
            "update chamados set dataAbertura = @dataAbertura, " +
            "descricaoProblema = @descricaoProblema, " +
            "descricaoAtendimento = @descricaoAtendimento," +
            "dataAtendimento = @dataAtendimento," +
            "situacao = @situacao," +
            "usuarioId = @usuarioId where id = @id";
            HelperDAO.ExecutaSQL(sql, CriaParametros(chamado));
        }
        private SqlParameter[] CriaParametros(ChamadoViewModel chamado)
        {
            SqlParameter[] parametros = new SqlParameter[7];

            parametros[0] = new SqlParameter("id", chamado.Id);
            parametros[1] = new SqlParameter("dataAbertura", chamado.DataAbertura);
            parametros[2] = new SqlParameter("descricaoProblema", chamado.DescricaoProblema);

            if (string.IsNullOrEmpty(chamado.DescricaoAtendimento))
                parametros[3] = new SqlParameter("descricaoAtendimento", DBNull.Value);
            else
                parametros[3] = new SqlParameter("descricaoAtendimento", chamado.DescricaoAtendimento);
            
            if (chamado.DataAtendimento == null)
                parametros[4] = new SqlParameter("dataAtendimento", DBNull.Value);
            else
                parametros[4] = new SqlParameter("dataAtendimento", chamado.DataAtendimento);

            parametros[5] = new SqlParameter("situacao", chamado.Situacao);

            if (chamado.UsuarioId == null)
                parametros[6] = new SqlParameter("usuarioId", DBNull.Value);
            else
                parametros[6] = new SqlParameter("usuarioId", chamado.UsuarioId);

            return parametros;
        }
        public void Excluir(int id)
        {
            string sql = "delete from chamados where id = @id";
            SqlParameter[] p = { new SqlParameter("id", id) };
            HelperDAO.ExecutaSQL(sql, p);
        }
        private ChamadoViewModel MontaChamado(DataRow registro)
        {
            ChamadoViewModel chamado = new ChamadoViewModel();

            chamado.Id = Convert.ToInt32(registro["id"]);
            chamado.DataAbertura = Convert.ToDateTime(registro["dataAbertura"]);
            chamado.DescricaoProblema = registro["descricaoProblema"].ToString();
            chamado.Situacao = Convert.ToInt32(registro["situacao"]);

            if (registro["descricaoAtendimento"] != DBNull.Value)
                chamado.DescricaoAtendimento = registro["descricaoAtendimento"].ToString();

            if (registro["dataAtendimento"] != DBNull.Value)
                chamado.DataAtendimento = Convert.ToDateTime(registro["dataAtendimento"]);

            if (registro["usuarioId"] != DBNull.Value)
                chamado.UsuarioId = Convert.ToInt32(registro["usuarioId"]);

            return chamado;
        }

        public ChamadoViewModel Consulta(int id)
        {
            string sql = "select * from chamados where id = @id";
            SqlParameter[] p = { new SqlParameter("id", id) };
            DataTable tabela = HelperDAO.ExecutaSelect(sql, p);

            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaChamado(tabela.Rows[0]);
        }
        public List<ChamadoViewModel> Listagem()
        {
            List<ChamadoViewModel> lista = new List<ChamadoViewModel>();
            string sql = "select * from chamados order by dataAbertura";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaChamado(registro));
            return lista;
        }

        public int ProximoId()
        {
            string sql = "select isnull(max(id) +1, 1) as 'MAIOR' from chamados";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }

    }
}
