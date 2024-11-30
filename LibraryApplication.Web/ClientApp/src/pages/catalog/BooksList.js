import React, {useEffect, useState} from 'react';
import AddBookModal from "./AddBookModal";
import {Button, List} from "antd";
import BookCard from "./BookCard";
import {API} from "../../configs/axios.config";
import {PlusOutlined} from "@ant-design/icons";
import {useUser} from "../../hooks/useUser";
import Title from "antd/es/typography/Title";

const BooksList = () => {
    const { user } = useUser();

    const [books, setBooks] = useState([])
    const [authors, setAuthors] = useState([])
    const [genres, setGenres] = useState([])
    const [showAddBookModal, setShowAddBookModal] = useState(false)
    
    const getBooks = async () => {
        const {data: newBooks} = await API.get(`/api/Book/all`)
        setBooks(newBooks)
    }

    useEffect(() => {
        (async () => {
            try {
                await getBooks();
            } catch (e) {
                console.log(e)
            }
        })()
    }, [])

    useEffect(() => {
        (async () => {
            try {
                const {data: newGenres} = await API.get(`/api/Genre/all`)
                setGenres(newGenres)
            } catch (e) {
                console.log(e)
            }
        })()
    }, [])

    useEffect(() => {
        (async () => {
            try {
                const {data: newAuthors} = await API.get(`/api/Author/all`)
                setAuthors(newAuthors)
            } catch (e) {
                console.log(e)
            }
        })()
    }, [])

    return (
        <div>
            <div style={{display: 'flex', justifyContent: 'space-between'}}>
                <Title level={2}>Catalog</Title>
                {user?.isAdmin && <Button className='mb-lg-4' onClick={() => setShowAddBookModal(true)} type='primary'><PlusOutlined/> Add Book</Button>}
            </div>
            {showAddBookModal && <AddBookModal setShowModal={setShowAddBookModal} setBooks={setBooks}/>}
            <List
                grid={{
                    gutter: 16,
                    column: 4,
                }}
                dataSource={books}
                renderItem={(book) => (
                    <List.Item>
                        <BookCard book={book} authors={authors} genres={genres} getBooks={getBooks}/>
                    </List.Item>
                )}
            />
        </div>
    );
};

export default BooksList;