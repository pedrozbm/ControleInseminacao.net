using ControleInseminacao.net.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleInseminacao.net.Controllers
{
    public class FazendaController : Controller
    {
        // GET: Fazenda
        public ActionResult Index()
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();
            var Fazenda = banco.Fazenda;
            return View(Fazenda);
          
        }
        public ActionResult Details(int id)
        {
            var Fazenda = new ControleInseminacaoEntities1().Fazenda.Where(x => x.IDFazenda == id).SingleOrDefault();
            return View(Fazenda);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]


        public ActionResult Create(Fazenda fazendas)
        {

            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();
            banco.Fazenda.Add(fazendas);

            banco.SaveChanges();

            return RedirectToAction("Details", new { id = fazendas.IDFazenda });
        }



        // GET: Fazenda/Edit/5


        [HttpGet]
        public ActionResult Edit(int id)
        {
            //estancia a classe do banco
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

            //busca a cidade
            var Fazendas = banco.Fazenda.Where(w => w.IDFazenda == id).SingleOrDefault();
            return View(Fazendas);
        }

        [HttpPost]
        public ActionResult Edit(Fazenda fazenda)
        {
            //estancia a classe do banco
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

            // busca a cidade linkada no banco de dados
            var FazendanoBanco = banco.Fazenda.Where(w => w.IDFazenda
            == fazenda.IDFazenda).SingleOrDefault();

            //altera os dados para o que veio
            //FazendanoBanco.IDFazenda = fazenda.IDFazenda;
            FazendanoBanco.Nome = fazenda.Nome;
          

            //salva no banco
            banco.SaveChanges();


            return RedirectToAction("Details", new { id = fazenda.IDFazenda });
        }
    


// GET: Fazenda/Delete/5
public ActionResult Delete(int id)
        {
        //estancia a classe do banco
        ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

        //busca 
        var fazendas = banco.Fazenda.Where(x => x.IDFazenda == id).SingleOrDefault();

        //apaga
        banco.Fazenda.Remove(fazendas);

        //salva o banco de dados
        banco.SaveChanges();

        //abre a tela de listar
        return RedirectToAction("Index");
        }

        // POST: Fazenda/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
