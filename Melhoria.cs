using System;
using System.Linq;

public class Class1
{
    decimal businessEstablishmentTax;

    public Class1()
    {
        if (movimento.QuantidadeParcela == 1)
            businessEstablishmentTax = 3.9m;
        else if (movimento.QuantidadeParcela == 2)
            businessEstablishmentTax = 5.9m;
        else if (movimento.QuantidadeParcela == 3)
            businessEstablishmentTax = 6.9m;
        else if (movimento.QuantidadeParcela == 4)
            businessEstablishmentTax = 7.9m;
        else if (movimento.QuantidadeParcela == 5)
            businessEstablishmentTax = 8.7m;
        else if (movimento.QuantidadeParcela == 6)
            businessEstablishmentTax = 9.6m;
        else if (movimento.QuantidadeParcela == 7)
            businessEstablishmentTax = 9.9m;
        else if (movimento.QuantidadeParcela == 8)
            businessEstablishmentTax = 10.5m;
        else if (movimento.QuantidadeParcela == 9)
            businessEstablishmentTax = 11.4m;
        else if (movimento.QuantidadeParcela == 10)
            businessEstablishmentTax = 12.3m;
        else if (movimento.QuantidadeParcela == 11)
            businessEstablishmentTax = 12.6m;
        else if (movimento.QuantidadeParcela == 12)
            businessEstablishmentTax = 12.9m;

        const QuantidadeParcelas = {
            1 : return 3.9m ,
            2 : return 5.9 ,
    }


    // Novo objeto para as taxas - Pode ser salvo em na pasta das models do projeto
    public class Tax{
        public Tax() { }

        public int QtdParcela { get; set; }
        public decimal businessEstablishmentTax { get; set; }

    }

    // Lista das Taxas - ficar salvo na mesma classe da "model" anterios
    List<Tax> list = new List
    {
        new Tax() {QtdParcela = 1, businessEstablishmentTax = 3.9m},
        new Tax() {QtdParcela = 2, businessEstablishmentTax = 5.9m},
        new Tax() {QtdParcela = 3, businessEstablishmentTax = 6.9m},
        new Tax() {QtdParcela = 4, businessEstablishmentTax = 7.9m},
        new Tax() {QtdParcela = 5, businessEstablishmentTax = 8.7m},
        new Tax() {QtdParcela = 6, businessEstablishmentTax = 9.6m},
        new Tax() {QtdParcela = 7, businessEstablishmentTax = 9.9m},
        new Tax() {QtdParcela = 8, businessEstablishmentTax = 3.9m},
        new Tax() {QtdParcela = 9, businessEstablishmentTax = 10.5m},
        new Tax() {QtdParcela = 10, businessEstablishmentTax = 11.4m},
        new Tax() {QtdParcela = 11, businessEstablishmentTax = 12.6m},
        new Tax() {QtdParcela = 12, businessEstablishmentTax = 12.9m}
    } 

    //Dentro do If
    if(movimento.CanalEntrada = "W"){
        var query = list.AsQueryable().Where(x => x.QtdParcela == movimento.QuantidadeParcela).FirstOrDefault();
        businessEstablishmentTax = query.businessEstablishmentTax;
    }

}
