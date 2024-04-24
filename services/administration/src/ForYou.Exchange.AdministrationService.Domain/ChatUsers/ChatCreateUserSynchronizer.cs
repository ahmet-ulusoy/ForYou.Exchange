using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;
using Volo.Chat.Users;

namespace ForYou.Exchange.AdministrationService.ChatUsers
{
    public class ChatCreateUserSynchronizer : IDistributedEventHandler<EntityCreatedEto<UserEto>>, ITransientDependency
    {
        protected IChatUserRepository UserRepository { get; }

        protected IChatUserLookupService UserLookupService { get; }

        public ChatCreateUserSynchronizer(
            IChatUserRepository userRepository,
            IChatUserLookupService userLookupService)
        {
            UserRepository = userRepository;
            UserLookupService = userLookupService;
        }

        public virtual async Task HandleEventAsync(EntityCreatedEto<UserEto> eventData)
        {
            var user = await UserRepository.FindAsync(eventData.Entity.Id);
            if (user == null)
            {
                user = new ChatUser(eventData.Entity);
                await UserRepository.InsertAsync(user);
            }
        }
    }
}
