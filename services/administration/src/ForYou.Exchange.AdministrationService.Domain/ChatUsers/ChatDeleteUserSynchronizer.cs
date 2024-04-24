using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;
using Volo.Chat.Users;

namespace ForYou.Exchange.AdministrationService.ChatUsers
{
    public class ChatDeleteUserSynchronizer : IDistributedEventHandler<EntityDeletedEto<UserEto>>, ITransientDependency
    {
        protected IChatUserRepository UserRepository { get; }

        protected IChatUserLookupService UserLookupService { get; }

        public ChatDeleteUserSynchronizer(
            IChatUserRepository userRepository,
            IChatUserLookupService userLookupService)
        {
            UserRepository = userRepository;
            UserLookupService = userLookupService;
        }

        public virtual async Task HandleEventAsync(EntityDeletedEto<UserEto> eventData)
        {
            var user = await UserRepository.FindAsync(eventData.Entity.Id);
            if (user == null)
            {
                eventData.Entity.IsActive = false;
                user = new ChatUser(eventData.Entity);
                await UserRepository.InsertAsync(user);
            }
            else
            {
                eventData.Entity.IsActive = false;
                user = new ChatUser(eventData.Entity);
                await UserRepository.UpdateAsync(user);
            }
        }
    }
}
