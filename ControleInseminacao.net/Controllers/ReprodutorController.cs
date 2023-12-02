using ControleInseminacao.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleInseminacao.net.Controllers
{
    public class ReprodutorController : Controller
    {
        // GET: Reprodutor
        public ActionResult Index()
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();
            var reprodutor = banco.Reprodutor.ToList(); 


            return View(reprodutor);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

            var reprodutor = banco.Reprodutor.ToList();
            
            ViewBag.ListaRacas = new SelectList(banco.IDRaca.ToList(), "IDRaca1", "Nome");

            // Preenche a lista suspensa para FK_Rep_IDFazenda
            ViewBag.ListaFazendas = new SelectList(banco.Fazenda.ToList(), "IDFazenda", "Nome");


            var lista = new List<SelectListItem>();

            foreach (var rep in reprodutor)
            {
                lista.Add(new SelectListItem { Value = rep.IDRep.ToString(), Text = rep.Nome });
            }

            ViewBag.IDCidade = lista;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Reprodutor reprodutor)
        {
            //estancia a classe do banco
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();
            banco.Reprodutor.Add(reprodutor);
            ViewBag.ListaRacas = new SelectList(banco.IDRaca.ToList(), "IDRaca", "Nome");

            // Preenche a lista suspensa para FK_Rep_IDFazenda
            ViewBag.ListaFazendas = new SelectList(banco.Fazenda.ToList(), "IDFazenda", "Nome");



            //salva no banco
            banco.SaveChanges();

            return RedirectToAction("Details", new { id = reprodutor.IDRep });
        }

        public ActionResult Details(int id) { 
        
     
            var reprodutor = new ControleInseminacaoEntities1().Reprodutor.Where(x => x.IDRep == id).SingleOrDefault();
            return View(reprodutor);
                }
        [HttpPost]


        [HttpGet]
        public ActionResult Edit(int id)
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

            var reprodutor = banco.Reprodutor.Include("Fazenda").Include("IDRaca").Where(w => w.IDRep == id).SingleOrDefault();

            var racas = banco.IDRaca.ToList();

            var lista = new List<SelectListItem>();

            foreach (var raca in racas)
            {
                lista.Add(new SelectListItem
                {
                    Value = raca.IDRaca1.ToString(),
                    Text = raca.Nome,
                    Selected = raca.IDRaca1 == reprodutor.IDRaca.IDRaca1
                });
            }

            ViewBag.Racas = lista;

            return View(reprodutor);
        }

        [HttpPost]
        public ActionResult Edit(Reprodutor reprodutor)
        {
            ControleInseminacaoEntities1 banco = new ControleInseminacaoEntities1();

            // Obtenha o Reprodutor do banco de dados incluindo as entidades relacionadas
            var reprodutorBD = banco.Reprodutor.Include("Fazenda").Include("IDRaca").Where(w => w.IDRep == reprodutor.IDRep).SingleOrDefault();

            // Atualize as propriedades do Reprodutor
            reprodutorBD.Nome = reprodutor.Nome;

            // Atualize as propriedades da Fazenda associada
            reprodutorBD.Fazenda.Nome = reprodutor.Fazenda.Nome;

            // Atualize as propriedades da Raça associada
            reprodutorBD.IDRaca.Nome = reprodutor.IDRaca.Nome;

            banco.SaveChanges();

            return RedirectToAction("Details", new { id = reprodutorBD.IDRep });
        }


    }



}