import React, {useEffect, useState} from 'react';
import {Button, Divider, Form, Input, InputNumber, Modal, notification, Select, Space, Switch} from "antd";
import { PlusOutlined } from '@ant-design/icons';
import {API} from "../../configs/axios.config";

const EditBookModal = ({book, setShowModal, getBooks}) => {
    const [authors, setAuthors] = useState([]);
    const [addAuthorNameInputValue, setAddAuthorNameInputValue] = useState('');
    const [addAuthorSurnameInputValue, setAddAuthorSurnameInputValue] = useState('');

    const [genres, setGenres] = useState([]);
    const [addGenreInputValue, setAddGenreInputValue] = useState('');

    useEffect(() => {
        (async () => {
            try {
                const {data: authors} = await API.get(`/api/Author/all`)
                setAuthors(authors)
            } catch (e) {
                console.log(e)
            }
        })()
    }, [])

    useEffect(() => {
        (async () => {
            try {
                const {data: genres} = await API.get(`/api/Genre/all`)
                setGenres(genres)
            } catch (e) {
                console.log(e)
            }
        })()
    }, [])

    const addItem = async (e) => {
        e.preventDefault();
        try {
            const {data: newAuthorId} = await API.post(`/api/Author/create`, {
                name: addAuthorNameInputValue,
                surname: addAuthorSurnameInputValue,
            })
            const {data: newAuthor} = await API.get(`/api/Author/${newAuthorId}`)

            setAuthors(prevAuthors => [...prevAuthors, newAuthor])
        } catch (e) {
            console.log(e)
        }
        setAddAuthorNameInputValue('');
        setAddAuthorSurnameInputValue('');
    };

    const addGenre = async (e) => {
        e.preventDefault();
        try {
            const {data: newGenreId} = await API.post(`/api/Genre/create`, {
                name: addGenreInputValue,
            })
            const {data: newGenre} = await API.get(`/api/Genre/${newGenreId}`)

            setGenres(prevGenres => [...prevGenres, newGenre])
        } catch (e) {
            console.log(e)
        }
        setAddAuthorNameInputValue('');
        setAddAuthorSurnameInputValue('');
    };

    const onFinish = async (values) => {
        try {
            await API.put(`/api/Book/${book.id}/edit`, {
                name: values.name,
                authorId: values.author,
                genreId: values.genre,
                rentPrice: values.rentPrice,
                isAvailable: values.isAvailable,
            })
            await getBooks();
            setShowModal(false)
            notification.success({message: 'Book saved!'})
        } catch (e) {
            console.log(e)
        }
    }

    const renderAuthorMenu = (menu) => (
        <>
            {menu}
            <Divider
                style={{
                    margin: '8px 0',
                }}
            />
            <Space
                style={{
                    padding: '0 8px 4px',
                }}
            >
                <Input
                    placeholder="Name"
                    value={addAuthorNameInputValue}
                    onChange={event => setAddAuthorNameInputValue(event.target.value)}
                />
                <Input
                    placeholder="Surname"
                    value={addAuthorSurnameInputValue}
                    onChange={event => setAddAuthorSurnameInputValue(event.target.value)}
                />
                <Button type="text" icon={<PlusOutlined />} onClick={addItem}>
                    Add author
                </Button>
            </Space>
        </>
    )

    const renderGenreMenu = (menu) => (
        <>
            {menu}
            <Divider
                style={{
                    margin: '8px 0',
                }}
            />
            <Space
                style={{
                    padding: '0 8px 4px',
                }}
            >
                <Input
                    placeholder="Name"
                    value={addGenreInputValue}
                    onChange={event => setAddGenreInputValue(event.target.value)}
                />
                <Button type="text" icon={<PlusOutlined />} onClick={addGenre}>
                    Add genre
                </Button>
            </Space>
        </>
    )

    return (
        <Modal
            title="Edit book"
            open={true}
            onCancel={() => setShowModal(false)}
            footer={null}
        >
            <Form
                name="create-book"
                initialValues={{
                    remember: true,
                }}
                onFinish={onFinish}
                requiredMark={false}
                layout="vertical"
            >
                <Form.Item
                    name="name"
                    label="Name"
                    rules={[
                        {
                            required: true,
                            message: 'Please input book name!',
                        },
                    ]}
                    initialValue={book.name}
                >
                    <Input placeholder="Name" />
                </Form.Item>

                <Form.Item
                    label="Author"
                    name="author"
                    rules={[{required: true, message: 'Please, select author of the book'}]}
                    initialValue={book.authorId}
                >
                    <Select
                        style={{
                            width: '100%',
                        }}
                        placeholder="Select author"
                        dropdownRender={renderAuthorMenu}
                        options={authors.map((author) => ({
                            label: author.name + ' ' + author.surname,
                            value: author.id,
                        }))}
                    />
                </Form.Item>

                <Form.Item
                    label="Genre"
                    name="genre"
                    rules={[{required: true, message: 'Please, select genre of the book'}]}
                    initialValue={book.genreId}
                >
                    <Select
                        style={{
                            width: '100%',
                        }}
                        placeholder="Select genre"
                        dropdownRender={renderGenreMenu}
                        options={genres.map((genre) => ({
                            label: genre.name,
                            value: genre.id,
                        }))}
                    />
                </Form.Item>
                <Form.Item
                    name="rentPrice"
                    label="Rent rice"
                    rules={[{required: true, message: 'Please, enter rent price of the book'}]}
                    initialValue={book.rentPrice}

                >
                    <InputNumber prefix="$"/>
                </Form.Item>
                <Form.Item name="isAvailable" label="Available" valuePropName="checked" initialValue={book.isAvailable}>
                    <Switch/>
                </Form.Item>
                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        Save
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default EditBookModal;