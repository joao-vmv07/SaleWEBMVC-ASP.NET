using System.Collections.Generic;


namespace SalesWebMVC.Models.ViewModels
{
    public class SellerFormViewModel //Classe que comtém os dados para o cadastro do vendedor
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }

    }
}
