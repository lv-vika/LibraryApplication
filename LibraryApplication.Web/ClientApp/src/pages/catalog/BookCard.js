import React, {useState} from 'react';
import {Button, Card, notification, Statistic} from "antd";
import {BookOutlined, DeleteOutlined, EditOutlined} from "@ant-design/icons";
import {useUser} from "../../hooks/useUser";
import BorrowBookModal from "./BorrowBookModal";
import {API} from "../../configs/axios.config";
import Paragraph from "antd/es/typography/Paragraph";
import EditBookModal from "./EditBookModal";

const BookCard = ({book, getBooks, authors, genres}) => {
    const { user } = useUser();

    const [showBorrowBookModal, setShowBorrowBookModal] = useState(false)
    const [showEditBookModal, setShowEditBookModal] = useState(false)

    const handleDeleteBook = async () => {
        try {
            const isSuccess = await API.delete(`/api/Book/${book.id}/delete`)
            isSuccess.data === true 
                ? notification.success({message: 'Book deleted!'})
                : notification.error({message: 'Failed to delete book!'})
            getBooks()
        } catch (e) {
            console.log(e)
        }
    }

    const cardActions = [<Button type='text' onClick={() => setShowBorrowBookModal(true)} icon={<BookOutlined key="borrow"  />} disabled={!book.isAvailable}/>]
    if (user?.isAdmin) {
        cardActions.unshift(<Button type='text' onClick={handleDeleteBook} icon={<DeleteOutlined key="delete"  />}/>)
        cardActions.unshift(<Button type='text' onClick={() => setShowEditBookModal(true)} icon={<EditOutlined key="edit" />}/>)
    }

    const author = authors.find(author => author.id === book.authorId)
    const genre = genres.find(genre => genre.id === book.genreId)

    return (
        <Card
            title={book.name}
            actions={cardActions}
        >
            {showBorrowBookModal && <BorrowBookModal book={book} setShowModal={setShowBorrowBookModal} getBooks={getBooks}/>}
            {showEditBookModal && <EditBookModal book={book} setShowModal={setShowEditBookModal} getBooks={getBooks}/>}
            <Paragraph>Author: {author?.name + " " + author?.surname}</Paragraph>
            <Paragraph>Genre: {genre?.name}</Paragraph>
            <Statistic
                title="Rent price"
                value={book.rentPrice}
                prefix="$"
            />
        </Card>
    );
};

export default BookCard;