using MainGame.Scripts.Logic.Npc;

namespace MainGame.Scripts.Logic.Tables.ManagerTable
{
    public class CustomerObserver
    {
        private bool _hasCustomer;
        private CustomerService _customerService;

        public Customer Customer { get; private set; }

        public void TakeNewCustomer(Customer customer)
        {
            if (Customer != null)
                return;
            
            Customer = customer;
            Customer.Reached += TakeCustomer;
        }

        private void TakeCustomer()
        {
            Customer.Reached -= TakeCustomer;
            _hasCustomer = true;
        }

        private void UnTakeCustomer()
        {
            Customer = null;
            _hasCustomer = false;
        }
    }
}