using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions.Events
{
    public delegate Task MessagePublishingEventHandler( object sender, MessagePublishingEventArgs e );

}