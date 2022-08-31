export interface message {
    id: string;
    senderId: string;
    senderUsername: string;
    senderPhotoUrl: string;
    recipientId: string;
    recipientUsername: string;
    recipientPhotoUrl: string;
    content: string;
    messageSent: Date;
    dateRead?: any;
}