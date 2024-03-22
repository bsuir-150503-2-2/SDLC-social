import React, { useState, useEffect } from 'react';
import axios from 'axios';

function Chat() {
    const [messageText, setMessageText] = useState('');
    const [chats, setChats] = useState([]);
    const [selectedChatId, setSelectedChatId] = useState(null);
    const [messages, setMessages] = useState([]);

    // Функция для загрузки списка чатов
    const fetchChats = async () => {
        try {
            const response = await axios.get('http://localhost:5006/api/Message/chats');
            setChats(response.data);
        } catch (error) {
            console.error('Error fetching chats:', error);
        }
    };

    // Функция для загрузки сообщений выбранного чата
    const fetchMessages = async (chatId) => {
        try {
            const response = await axios.get(`http://localhost:5006/api/Message/chat/${chatId}`);
            setMessages(response.data);
        } catch (error) {
            console.error('Error fetching messages:', error);
        }
    };

    // Обработчик отправки сообщения
    const sendMessage = async () => {
        try {
            await axios.post('http://localhost:5006/api/Message/send', {
                ReceiverId: selectedChatId, // Используем выбранный чат в качестве получателя
                Content: messageText
            });
            // После отправки обновляем список сообщений
            fetchMessages(selectedChatId);
        } catch (error) {
            console.error('Error sending message:', error);
        }
    };

    // Загружаем список чатов при монтировании компонента
    useEffect(() => {
        fetchChats();
    }, []);

    // Обработчик выбора чата
    const handleChatSelect = (chatId) => {
        setSelectedChatId(chatId);
        fetchMessages(chatId);
    };

    return (
        <div>
            <h1>Chat Application</h1>
            <div>
                <h2>Chats</h2>
                <ul>
                    {chats.map(chat => (
                        <li key={chat.chatId} onClick={() => handleChatSelect(chat.chatId)}>
                            {chat.chatName}
                        </li>
                    ))}
                </ul>
            </div>
            <div>
                <h2>Messages</h2>
                <ul>
                    {messages.map(message => (
                        <li key={message.messageId}>{message.content}</li>
                    ))}
                </ul>
            </div>
            <div>
                <input
                    type="text"
                    value={messageText}
                    onChange={(e) => setMessageText(e.target.value)}
                />
                <button onClick={sendMessage}>Send</button>
            </div>
        </div>
    );
}

export default Chat;
