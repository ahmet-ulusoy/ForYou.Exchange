using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Chat.Messages;

namespace ForYou.Exchange.AdministrationService.Chat;

public class MsChatMessageDistributedEventHandler :
    IDistributedEventHandler<ChatMessageEto>,
    IDistributedEventHandler<ChatDeletedMessageEto>,
    IDistributedEventHandler<ChatDeletedConversationEto>,
    ITransientDependency
{
    private readonly SignalRRealTimeChatMessageSender _realTimeChatMessageSender;

    public MsChatMessageDistributedEventHandler(
        SignalRRealTimeChatMessageSender realTimeChatMessageSender)
    {
        _realTimeChatMessageSender = realTimeChatMessageSender;
    }

    public async Task HandleEventAsync(ChatMessageEto eventData)
    {
        await _realTimeChatMessageSender.SendAsync(
            eventData.TargetUserId,
            new ChatMessageRdto
            {
                Id = eventData.MessageId,
                SenderUserId = eventData.SenderUserId,
                SenderUsername = eventData.SenderUserName,
                SenderName = eventData.SenderName,
                SenderSurname = eventData.SenderSurname,
                Text = eventData.Message
            }
        );
    }

    public async Task HandleEventAsync(ChatDeletedMessageEto eventData)
    {
        await _realTimeChatMessageSender.DeleteMessageAsync(eventData.TargetUserId, eventData.MessageId);
    }

    public async Task HandleEventAsync(ChatDeletedConversationEto eventData)
    {
        await _realTimeChatMessageSender.DeleteConversationAsync(eventData.TargetUserId, eventData.UserId);
    }
}
