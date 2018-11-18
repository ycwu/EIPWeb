namespace EIPWeb.Models
{
	public static class RepositoryHelper
	{
		public static IUnitOfWork GetUnitOfWork()
		{
			return new EFUnitOfWork();
		}		
		
		public static ChatClientRepository GetChatClientRepository()
		{
			var repository = new ChatClientRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ChatClientRepository GetChatClientRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ChatClientRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ChatMessageRepository GetChatMessageRepository()
		{
			var repository = new ChatMessageRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ChatMessageRepository GetChatMessageRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ChatMessageRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ChatroomRepository GetChatroomRepository()
		{
			var repository = new ChatroomRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ChatroomRepository GetChatroomRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ChatroomRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
		}		

		public static ChatroomDetailRepository GetChatroomDetailRepository()
		{
			var repository = new ChatroomDetailRepository();
			repository.UnitOfWork = GetUnitOfWork();
			return repository;
		}

		public static ChatroomDetailRepository GetChatroomDetailRepository(IUnitOfWork unitOfWork)
		{
			var repository = new ChatroomDetailRepository();
			repository.UnitOfWork = unitOfWork;
			return repository;
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