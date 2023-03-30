using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetDocsExpediente
{
    class SharepointController
    { 

        private ClientContext context;

        public SharepointController(String SiteUrl)
        {
            context = new ClientContext(SiteUrl);
        }

        public void Login(string User, string Password)  
        {
            context.Credentials = new NetworkCredential(User, Password);
            context.ExecuteQuery();
        }

        public int GetIdExpediente(string nombreLista, string nombreExpediente)
        {
            int id = -1;

            var lista = context.Web.Lists.GetByTitle(nombreLista);

            string camlQuery = $"<View><Query><Where><Eq><FieldRef Name='EXPEDIENTE'/><Value Type='Text'>{nombreExpediente}</Value></Eq></Where></Query></View>";
            CamlQuery query = new CamlQuery();
            query.ViewXml = camlQuery;
            
            var items = lista.GetItems(query);
            context.Load(items);
            context.ExecuteQuery();

            var expedientes = items.ToList();
            
            //Que pasa si devuelve más de un elemento?
            if(expedientes.Count == 1)
            {
                return (int)expedientes[0]["ID"];
            }

            return id;
        }

        public List<string> GetListaDocumentos(int idExpediente, string nombreLista, string ctype)
        {

            var lista = context.Web.Lists.GetByTitle(nombreLista);

            string camlQuery = $"<View><Query><Where><Eq><FieldRef Name='Title'/><Value Type='Text'>{idExpediente}</Value></Eq></Where></Query></View>";
            CamlQuery query = new CamlQuery();
            query.ViewXml = camlQuery;

            var items = lista.GetItems(query);
            context.Load(items);
            context.ExecuteQuery();

            var documentos = from documento in items.ToList()
                        select new
                        {
                            IDDocumento = corrigeId($"{documento["ID"]}"),
                            URLDocumento = documento["UrlDocumento"],
                            NombreDocumento = documento["Descripcion"],
                            PuntoDocumental = corrigeId($"{documento["IdTipodeDocumento"]}")
                        };

            string strIdExpediente = corrigeId($"{idExpediente}");
            List<string> result = new List<string>();
            documentos.ToList().ForEach(documento =>
            {
                result.Add($"Servidor\\{ctype}\\{documento.PuntoDocumental}\\{strIdExpediente}.{documento.URLDocumento} # {documento.NombreDocumento} # {documento.PuntoDocumental}");
            });

            return result;
        }

        private string corrigeId(string Id)
        {

            string resId = Id;

            while(resId.Length < 10)
            {
                resId = "0" + resId;
            }

            return resId;
        }


        /*
         public void imprimelistas()
        {
            Web web = context.Web;
            context.Load(web.Lists, lists => lists.Include(list => list.Title));
            context.ExecuteQuery();

            foreach (List list in web.Lists)
            {
                Console.WriteLine(list.Title);
            }
            Console.Read();
        }

        public void imprimecolumnas(string listName)
        {
            List list = context.Web.Lists.GetByTitle(listName);
                context.Load(list);

                // Cargar las columnas de la lista
                context.Load(list.Fields);
                context.ExecuteQuery();

                // Mostrar las columnas de la lista
                foreach (Field field in list.Fields)
                {
                    Console.WriteLine("Columna: " + field.Title);
                }
            
            Console.ReadLine();
        }
         */

    }
}
