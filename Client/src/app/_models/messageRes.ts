export interface messageRes {
    id: string;
    senderId: string;
    serderUsername: string;
    sender?: any;
    recipientId: string;
    recipientUsername: string;
    recipient?: any;
    content: string;
    dateRead?: any;
    messageSent: Date;
    senderDeleted: boolean;
    recipientDeleted: boolean;
}

