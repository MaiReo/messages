using System.Threading.Tasks;

namespace MaiReo.Messages.Abstractions.Events
{
    public delegate Task MessageReceivingEventHandler( object sender, MessageReceivingEventArgs e );
}