﻿using ControleContatos.Data;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;    
        }
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuario
                .Include(x=> x.Contatos)
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _bancoContext.Usuario.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null)
                throw new System.Exception("Houve um erro na atualização do usuario");

            usuarioDB.Nome            = usuario.Nome;
            usuarioDB.Email           = usuario.Email;
            usuarioDB.Login           = usuario.Login;
            usuarioDB.DataAtualizacao = DateTime.Now;
            usuarioDB.Perfil          = usuario.Perfil;

            _bancoContext.Usuario.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null)
                throw new System.Exception("Houve um erro na exclusão do usuário");

            _bancoContext.Usuario.Remove(usuarioDB);
            _bancoContext.SaveChanges();
            return true;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null)
                throw new System.Exception("Houve um erro na atualização a senha do usuario");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual))
                throw new System.Exception("Senha atual não confêre!");


            if (!usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha))
                throw new System.Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.Senha = alterarSenhaModel.NovaSenha;
            usuarioDB.SetSenhaHash();
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuario.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }
    }
}
