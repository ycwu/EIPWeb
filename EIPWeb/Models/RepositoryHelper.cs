namespace EIPWeb.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static DepartmentRepository GetDepartmentRepository()
		{
			var repository = new DepartmentRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static DepartmentRepository GetDepartmentRepository(IUnitOfWork unitOfWork)
		{
			var repository = new DepartmentRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static EmployeeRepository GetEmployeeRepository()
		{
			var repository = new EmployeeRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static EmployeeRepository GetEmployeeRepository(IUnitOfWork unitOfWork)
		{
			var repository = new EmployeeRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		
	}
}