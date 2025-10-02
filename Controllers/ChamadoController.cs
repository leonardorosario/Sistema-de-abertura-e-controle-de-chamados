using CadAlunoMVC.DAO;
using CadAlunoMVC.Models;
using CadChamadoMVC.DAO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace CadAlunoMVC.Controllers
{
    public class ChamadoController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ChamadoDAO dao = new ChamadoDAO();
                List<ChamadoViewModel> lista = dao.Listagem();
                return View("index", lista);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.Message));
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewBag.Operacao = "I";

                UsuarioDAO usuarioDAO = new UsuarioDAO();
                ViewBag.Usuarios = usuarioDAO.Listagem();

                ChamadoViewModel chamado = new ChamadoViewModel();
                chamado.DataAbertura = DateTime.Now;
                ChamadoDAO dAO = new ChamadoDAO();
                chamado.Id = dAO.ProximoId();
                ViewBag.Hoje = DateTime.Now.ToString("yyyy-MM-dd"); 
                return View("Form", chamado);
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.Message));
            }
        }

        private void ValidaDados(ChamadoViewModel chamado, string operacao)
        {
            ModelState.Clear(); // Limpa os erros automáticos para usarmos nossas mensagens personalizadas.

            if (operacao == "I" && chamado.Id <= 0)
                ModelState.AddModelError("id", "Id inválido!");
            else if (operacao == "A" && chamado.Id <= 0)
                ModelState.AddModelError("id", "Id inválido!");
            else
            {
                ChamadoDAO dao = new ChamadoDAO();
                if (operacao == "I" && dao.Consulta(chamado.Id) != null)
                    ModelState.AddModelError("id", "Este código de chamado já está em uso.");
                if (operacao == "A" && dao.Consulta(chamado.Id) == null)
                    ModelState.AddModelError("id", "Este chamado não existe.");
            }

            if (chamado.DataAbertura == DateTime.MinValue)
                ModelState.AddModelError("dataAbertura", "A data de abertura é obrigatória.");

            if (chamado.DataAbertura.Date > DateTime.Now.Date)
                ModelState.AddModelError("dataAbertura", "A data de abertura não pode ser uma data futura.");

            if (string.IsNullOrWhiteSpace(chamado.DescricaoProblema))
                ModelState.AddModelError("descricaoProblema", "A descrição do problema é obrigatória.");

            const int PENDENTE = 1;
            const int ATENDIDO = 2;

            if (chamado.Situacao != PENDENTE && chamado.Situacao != ATENDIDO)
                ModelState.AddModelError("situacao", "A situação do chamado é obrigatória e deve ser 'Pendente [1]' ou 'Atendido [2]'.");


            if (chamado.Situacao == ATENDIDO)
            {
                if (string.IsNullOrWhiteSpace(chamado.DescricaoAtendimento))
                    ModelState.AddModelError("descricaoAtendimento", "A descrição do atendimento é obrigatória quando a situação é 'Atendido'.");

                if (chamado.DataAtendimento == null)
                    ModelState.AddModelError("dataAtendimento", "A data do atendimento é obrigatória quando a situação é 'Atendido'.");

                if (chamado.UsuarioId == null || chamado.UsuarioId <= 0)
                    ModelState.AddModelError("usuarioId", "O código do usuário é obrigatório quando a situação é 'Atendido'.");
            }
        }


        public IActionResult Salvar(ChamadoViewModel chamado, string Operacao)
        {
            try
            {
                ValidaDados(chamado, Operacao);
                if (ModelState.ErrorCount == 0)
                {
                    ChamadoDAO dao = new ChamadoDAO();

                    if (Operacao == "I")
                        dao.Inserir(chamado);
                    else
                        dao.Alterar(chamado);

                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Operacao = Operacao;   
                    return View("Form", chamado);
                }
            }
            catch (Exception ex)
            {
                return View("error", new ErrorViewModel(ex.ToString()));
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operacao = "A";

                UsuarioDAO usuarioDAO = new UsuarioDAO();
                ViewBag.Usuarios = usuarioDAO.Listagem();

                ChamadoDAO dao = new ChamadoDAO();
                ChamadoViewModel chamado = dao.Consulta(id);
                if (chamado == null)
                    return RedirectToAction("index");
                else
                ViewBag.Hoje = DateTime.Now.ToString("yyyy-MM-dd");
                return View("Form", chamado);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                ChamadoDAO dao = new ChamadoDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

    }
}
