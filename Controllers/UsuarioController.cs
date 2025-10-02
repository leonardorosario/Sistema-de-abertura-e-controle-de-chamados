﻿using CadAlunoMVC.Models;
using CadChamadoMVC.DAO;
using Microsoft.AspNetCore.Mvc;
using System;


namespace CadChamadoMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            UsuarioDAO dao = new UsuarioDAO();
            var listaDeUsuarios = dao.Listagem();
            return View(listaDeUsuarios);
        }

        public ActionResult Create()
        {
            ViewBag.Operacao = "I";

            UsuarioDAO dao = new UsuarioDAO();
            UsuarioViewModel usuario = new UsuarioViewModel();
            usuario.Id = dao.ProximoId();
            return View("Form", usuario);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Operacao = "A";

            UsuarioDAO dao = new UsuarioDAO();
            UsuarioViewModel usuario = dao.Consulta(id);
            if (usuario == null)
                return RedirectToAction("index");
            else
                return View("Form", usuario);
        }

        public ActionResult Salvar(UsuarioViewModel usuario, string operacao)
        {
            try
            {
                ValidaDados(usuario);
                if (ModelState.IsValid)
                {
                    UsuarioDAO dao = new UsuarioDAO();
                    if (operacao == "I")
                        dao.Inserir(usuario);
                    else
                        dao.Alterar(usuario);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Operacao = operacao;

                    return View("Form", usuario);
                }
            }
            catch (Exception erro)
            {
                ModelState.AddModelError("Erro", "Ocorreu um erro: " + erro.Message);
                return View("Form", usuario);
            }
        }

        private void ValidaDados(UsuarioViewModel usuario)
        {
            ModelState.Clear();

            if (usuario.Id <= 0)
                ModelState.AddModelError("Id", "Campo obrigatório.");

            if (string.IsNullOrWhiteSpace(usuario.Nome))
                ModelState.AddModelError("Nome", "O nome é obrigatório.");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                dao.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}
