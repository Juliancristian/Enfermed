using System.Collections.Generic;
using Enfermed.Models;

namespace Enfermed.Data
{
    public class GuiaData
    {
        private List<Guia> _listItemsGuia;

        public GuiaData()
        {
            _listItemsGuia = new List<Guia>()
            {
                new Guia()
                {
                    Id = 1,
                    Title = "Aciclovir",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 500mg \nDiluyentes para reconstitución: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 50 \nESTABILIDAD: 25°C - 12HS \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 5-10 \nTIEMPO: 1 hora mínimo"
                },
                new Guia()
                {
                    Id = 2,
                    Title = "Amikacina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 100mg \n \nVIA DE ADMIN: IV \nPresentación: FA 500mg \nVOL. INFUSION: 50-200 \nCONCENT. INFUSION (mg/ml): 2,5-5 \nTIEMPO: 30 – 60 minutos"
                },
                new Guia()
                {
                    Id = 3,
                    Title = "Ampicilina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g  \nDiluyentes para reconstitución: AD \nVol. DIL (ml): 3,5 \nCONC. FINAL (mg/ml): 250 \nESTABILIDAD: 25°C - 1h  \n \nVIA DE ADMIN: IV directa \nPresentación: FA 1g  \nDiluyentes para reconstitución: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 1h / 2-8°C - 4h \nTIEMPO: 10-15 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 1g  \nDiluyentes para reconstitución: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 1h / 2-8°C - 4h \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 15-30 minutos"
                },
                new Guia()
                {
                    Id = 4,
                    Title = "Ampisulbactam",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1,5g \nDiluyentes para reconstitución: AD-Lidoc 1% \nVol. DIL (ml): 3,2 \nCONC. FINAL (mg/ml): 250 \nESTABILIDAD: 25°C - 1h  \n \nVIA DE ADMIN: IV directa \nPresentación: FA 1,5g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 3,2 \nCONC. FINAL (mg/ml): 250 \nESTABILIDAD: 25°C - 1h / 2-8°C - 4hs \nTIEMPO: 10-15 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 1,5g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 3,2 \nCONC. FINAL (mg/ml): 250 \nESTABILIDAD: 25°C - 1h / 2-8°C - 4h \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 15-30 minutos"
                },
                new Guia()
                {
                    Id = 5,
                    Title = "Anfotericina",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 50mg \nDiluyentes para reconstitucion: AD-Dx 5% \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 5 \nESTABILIDAD: 25°C – 24hs / 2-8°C - 7 ds \nVOL. INFUSION: 500 \nCONCENT. INFUSION (mg/ml): 0,1 \nTIEMPO: 6 horas"
                },
                new Guia()
                {
                    Id = 6,
                    Title = "Anfotericina Liposomal",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 50mg \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 12,0 \nESTABILIDAD: 25°C – 24hs \nVOL. INFUSION: 25-100 \nCONCENT. INFUSION (mg/ml): 2-0,2 \nTIEMPO: 4-6 horas"
                },
                new Guia()
                {
                    Id = 7,
                    Title = "Cefazolina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD-SF \nVol. DIL (ml): 2,5 \nESTABILIDAD: 25°C - 24h  2-8°C – 10 ds \n \nVIA DE ADMIN: IV directa \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 5,0-10,0 \nCONC. FINAL (mg/ml): 100-200 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 10 ds \nTIEMPO: 3-5 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 5,0-10,0 \nCONC. FINAL (mg/ml): 100-200 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 10 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 8,
                    Title = "Cefalotina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 4,0 \nCONC. FINAL (mg/ml): 225 \nESTABILIDAD: 25°C - 24h  2-8°C – 4 ds \n \nVIA DE ADMIN: IV directa \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD-SF-Dx 5% \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100-200 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 4 ds \nTIEMPO: 3-5 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD-SF-Dx 5%  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100-200 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 4 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 9,
                    Title = "Cefepine",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 2g  \nDiluyentes para reconstitucion: AD-SF-Dx 5%  \nVol. DIL (ml): 7,0 \nCONC. FINAL (mg/ml): 280 \n \nVIA DE ADMIN: IV directa \nPresentación: FA 2g \nDiluyentes para reconstitucion: AD-SF-Dx 5% \nVol. DIL (ml): 20,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nTIEMPO: 3-5 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 2g \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 200 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nVOL. INFUSION: 50-200 \nCONCENT. INFUSION (mg/ml): 10-40 \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 10,
                    Title = "Cefotaxima",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 3,0 \nCONC. FINAL (mg/ml): 300 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 10 ds \n \nVIA DE ADMIN: IV directa \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 95 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 10 ds \nTIEMPO: 3-5 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 95 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 10 ds \nVOL. INFUSION: 50-100 \n CONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 11,
                    Title = "Ceftadizima",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 3,0 \nCONC. FINAL (mg/ml): 280 \nESTABILIDAD: 25°C - 18hs / 2-8°C – 7 ds \n \nVIA DE ADMIN: IV directa \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nTIEMPO: 3-5 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 15-30 minutos"
                },
                new Guia()
                {
                    Id = 12,
                    Title = "Ceftriaxona",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 3,0 \nCONC. FINAL (mg/ml): 280 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 3 ds \n \nVIA DE ADMIN: IV \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C – 3 ds / 2-8°C – 10 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 15-30 minutos"
                },
                new Guia()
                {
                    Id = 13,
                    Title = "Claritromicina",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 500mg \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 50 \nESTABILIDAD: 25°C – 24hs / 2-8°C – 48hs \nVOL. INFUSION: 250 \nCONCENT. INFUSION (mg/ml): 2 \nTIEMPO: 1 hora"
                },
                new Guia()
                {
                    Id = 14,
                    Title = "Clindamicina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 600mg \n \nVIA DE ADMIN: IV \nPresentación: FA 600mg \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 18 máximo \nTIEMPO: 20-60 minutos"
                },
                new Guia()
                {
                    Id = 15,
                    Title = "Cloranfenicol",
                    Body = "VIA DE ADMIN: IV directa \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD-Dx 5%  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C – 30 ds \nTIEMPO: 1 minuto \n \nVIA DE ADMIN: IV \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD-Dx 5%  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C – 30 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 16,
                    Title = "Colistina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 100mg  \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 3,0 \nCONC. FINAL (mg/ml): 280 \nESTABILIDAD: 25°C - 18hs / 2-8°C – 7 ds \n \nVIA DE ADMIN: IV directa \nPresentación: FA 100mg  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nTIEMPO: 3-5 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 100mg \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 15-30 minutos"
                },
                new Guia()
                {
                    Id = 17,
                    Title = "Cotrimoxazol Trime-Sulfa",
                    Body = "VIA DE ADMIN: IV \nPresentación: Amp. 80 t / 400 s \nVOL. INFUSION: 100-200 \nCONCENT. INFUSION (mg/ml): 0,4-0,8 t \nTIEMPO: 60-90 minutos"
                },
                new Guia()
                {
                    Id = 18,
                    Title = "Estreptomicina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 4,2 \nCONC. FINAL (mg/ml): 200 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 48hs \n \nVIA DE ADMIN: IM \nPresentación: FA 1g  \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 3,2 \nCONC. FINAL (mg/ml): 250 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 48hs \n \nVIA DE ADMIN: IM \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 1,8 \nCONC. FINAL (mg/ml): 400 \nESTABILIDAD: 25°C - 24hs / 2-8°C – 48hs"
                },
                new Guia()
                {
                    Id = 19,
                    Title = "Ganciclovir",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 500mg \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 10 \nCONC. FINAL (mg/ml): 50 \nESTABILIDAD: 25°C – 12hs \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): 10/05/09 \nTIEMPO: 1 hora"
                },
                new Guia()
                {
                    Id = 20,
                    Title = "Gentamcina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 80mg \n \nVIA DE ADMIN: IV \nPresentación: FA 80mg  \nVOL. INFUSION: 50-200 \nCONCENT. INFUSION (mg/ml): variable \nTIEMPO: 30-120 minutos"
                },
                new Guia()
                {
                    Id = 21,
                    Title = "Imipenem",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 500mg \nDiluyentes para reconstitucion: SF \nVol. DIL (ml): 100 \nCONC. FINAL (mg/ml): 5 \nESTABILIDAD: 25°C - 10hs / 2-8°C – 48hs \nVOL. INFUSION: es el de reconstitución \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 30-60 minutos \n \nVIA DE ADMIN: IV \nPresentación: FA 500mg  \nDiluyentes para reconstitucion: Dx5% \nVol. DIL (ml): 100 \nCONC. FINAL (mg/ml): 5 \nESTABILIDAD: 25°C - 4hs / 2-8°C – 24hs \nVOL. INFUSION: es el de reconstitución \nCONCENT. INFUSION (mg/ml): 10-20 \nTIEMPO: 30-60 minutos"
                },
                new Guia()
                {
                    Id = 22,
                    Title = "Meropenem",
                    Body = "VIA DE ADMIN: IV directa \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 20,0 \nCONC. FINAL (mg/ml): 50 \nESTABILIDAD: 25°C - 8hs / 2-8°C – 48hs \n \nVIA DE ADMIN: IV \nPresentación: FA 1g \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 20,0 \nCONC. FINAL (mg/ml): 50 \nESTABILIDAD: 25°C - 8hs / 2-8°C – 48hs \nVOL. INFUSION: 50-200 \nCONCENT. INFUSION (mg/ml): 5-20 \nTIEMPO: 15-30 minutos"
                },
                new Guia()
                {
                    Id = 23,
                    Title = "Penicilina G",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 1M \nDiluyentes para reconstitucion: AD-SF-Dx5%  \nVol. DIL (ml): 4,5 \nCONC. FINAL (mg/ml): 200000U \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \n \nVIA DE ADMIN: IM \nPresentación: FA 3M \nDiluyentes para reconstitucion: AD-SF-Dx5%  \nVol. DIL (ml): 9,0 \nCONC. FINAL (mg/ml): 300000U \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \n \nVIA DE ADMIN: IV \nPresentación: FA 1M  \nDiluyentes para reconstitucion: AD-SF  \nVol. DIL (ml): 4,5 \nCONC. FINAL (mg/ml): 200000U \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): variable \nTIEMPO: 1-2 horas \n \nVIA DE ADMIN: IV \nPresentación: FA 3M \nDiluyentes para reconstitucion: AD-SF  \nVol. DIL (ml): 9,0 \nCONC. FINAL (mg/ml): 300000U \nESTABILIDAD: 25°C - 24hs / 2-8°C – 7 ds \nVOL. INFUSION: 50-100 \nCONCENT. INFUSION (mg/ml): variable \nTIEMPO: 1-2 horas"
                },
                new Guia()
                {
                    Id = 24,
                    Title = "Piperacilina – Tazobactam",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 4 g P / 0.5 T \nDiluyentes para reconstitucion: AD – SF – Dx5% \nVol. DIL (ml): 20,0 \nCONC. FINAL (mg/ml): 200 \nESTABILIDAD: 25°C – 14 ds 24hs / 2-8°C – 48hs \nVOL. INFUSION: 50 mínimo \nCONCENT. INFUSION (mg/ml): 80 mínimo \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 25,
                    Title = "Rifampicina",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 600mg \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 60 \nESTABILIDAD: 25°C - 24hs \nVOL. INFUSION: 100 \nCONCENT. INFUSION (mg/ml): 6 \nTIEMPO: 30 minutos mínimo \n \nVIA DE ADMIN: IV \nPresentación: FA 600mg \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 10,0 \nCONC. FINAL (mg/ml): 60 \nESTABILIDAD: 25°C - 24hs \nVOL. INFUSION: 500 \nCONCENT. INFUSION (mg/ml): 1-2 \nTIEMPO: 3 horas"
                },
                new Guia()
                {
                    Id = 26,
                    Title = "Teicoplanina",
                    Body = "VIA DE ADMIN: Intramuscular \nPresentación: FA 200/400mg \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 3 \nCONC. FINAL (mg/ml): 280 \nESTABILIDAD: 25°C - 48hs / 2-8°C – 7 ds \n \nVIA DE ADMIN: IV directa \nPresentación: FA 200/400mg \nDiluyentes para reconstitucion: AD \nVol. DIL (ml): 3 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 48hs / 2-8°C – 7 ds \nTIEMPO: 1 minuto \n \nVIA DE ADMIN: IV \nPresentación: FA 200/400mg \nDiluyentes para reconstitucion: AD  \nVol. DIL (ml): 3 \nCONC. FINAL (mg/ml): 100 \nESTABILIDAD: 25°C - 58hs / 2-8°C – 7 ds \nTIEMPO: 30 minutos"
                },
                new Guia()
                {
                    Id = 27,
                    Title = "Tigeciclina",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 50mg \nDiluyentes para reconstitucion: SF – Dx5% - RL \nVol. DIL (ml): 5,3 ml \nCONC. FINAL (mg/ml): 10 \nESTABILIDAD: 25°C – 6hs \nVOL. INFUSION: 100 \nCONCENT. INFUSION (mg/ml): 0,5 \nTIEMPO: 30-60 minutos"
                },
                new Guia()
                {
                    Id = 28,
                    Title = "Vancomicina",
                    Body = "VIA DE ADMIN: IV \nPresentación: FA 500mg \nDiluyentes para reconstitución: AD \nVol. DIL (ml): 10 \nCONC. FINAL (mg/ml): 50 \nESTABILIDAD: 25°C – 14 ds 24hs / 2-8°C – 14 ds \nVOL. INFUSION: 100-200 \nCONCENT. INFUSION (mg/ml): 5 max. \nTIEMPO: 1 hora mínimo"
                }
            };
        }

        // Devuelve Lista de Guia Data
        public List<Guia> getListGuiaData()
        {
            return _listItemsGuia;
        }
    }
}