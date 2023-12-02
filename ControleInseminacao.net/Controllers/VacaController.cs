using ControleInseminacao.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleInseminacao.net.Controllers
{
    public class VacaController : Controller
    {
        // GET: Vaca
        public ActionResult Index()
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();
            var Vacas = banco.Vaca.ToList();
            return View(Vacas);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

            var vaca = banco.Vaca.ToList();

            ViewBag.ListaRacas = new SelectList(banco.IDRaca.ToList(), "IDRaca1", "Nome");

            // Preenche a lista suspensa para FK_Rep_IDFazenda
            ViewBag.ListaFazendas = new SelectList(banco.Fazenda.ToList(), "IDFazenda", "Nome");


            var lista = new List<SelectListItem>();

            foreach (var vac in vaca)
            {
                lista.Add(new SelectListItem { Value = vac.IDVaca.ToString(), Text = vac.Nome });
            }

            ViewBag.IDVaca = lista;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Vaca vaca)
        {
            //estancia a classe do banco
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();
            
            ViewBag.ListaRacas = new SelectList(banco.IDRaca.ToList(), "IDRaca1", "Nome");

            // Preenche a lista suspensa para FK_Rep_IDFazenda
            ViewBag.ListaFazendas = new SelectList(banco.Fazenda.ToList(), "IDFazenda", "Nome");
            banco.Vaca.Add(vaca);


            //salva no banco
            banco.SaveChanges();

            return RedirectToAction("Details", new { id = vaca.IDVaca });
        }



    }
    
}
