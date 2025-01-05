﻿using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaParaUsurioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }  

        public IActionResult Criar()
        {
            return View();
        }   

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(contato);

                _contatoRepositorio.Adicionar(contato);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao cadastrar o contato, detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Editar", contato);

                _contatoRepositorio.Atualizar(contato);
                TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o contato, detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(id);

                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                else
                    TempData["MensagemErro"] = "Não foi possível apagar seu contato";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao apagar o contato, detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
