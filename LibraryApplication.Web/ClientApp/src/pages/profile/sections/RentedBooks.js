import React, { useEffect, useState } from "react";
import { Button, notification, Table, Tag } from "antd";
import {
  ArrowRightOutlined,
  CheckCircleOutlined,
  CloseCircleOutlined,
} from "@ant-design/icons";
import moment from "moment";
import { useUser } from "../../../hooks/useUser";
import { API } from "../../../configs/axios.config";
import Title from "antd/es/typography/Title";

const RentedBooks = () => {
  const { user } = useUser();

  const [books, setBooks] = useState([]);
  const [loading, setLoading] = useState(false);

  const getBooks = async () => {
    const { data: newBooks } = await API.get(
      `/api/User/${user?.id}/book-transfers`
    );
    setBooks(newBooks);
  };

  useEffect(() => {
    (async () => {
      setLoading(true);
      try {
        await getBooks();
      } catch (e) {
        console.log(e);
      }
      setLoading(false);
    })();
  }, []);

  const columns = [
    {
      title: "Book name",
      dataIndex: "bookName",
      key: "bookName",
    },
    {
      title: "Author",
      dataIndex: "author",
      key: "author",
    },
    {
      title: "Genre",
      dataIndex: "genre",
      key: "genre",
    },
    {
      title: "Rent price",
      dataIndex: "rentPrice",
      key: "rentPrice",
    },
    {
      title: "Rented on",
      dataIndex: "transferDate",
      key: "transferDate",
    },
    {
      title: "Expected return date",
      dataIndex: "expectedReturnDate",
      key: "expectedReturnDate",
    },
    {
      title: "Overdue",
      key: "overdue",
      dataIndex: "overdue",
      width: "96px",
      render: (overdue) => (
        <Tag
          icon={overdue ? <CloseCircleOutlined /> : <CheckCircleOutlined />}
          color={overdue ? "error" : "success"}
        >
          {overdue ? "OVERDUE" : "ON TIME"}
        </Tag>
      ),
    },
    {
      title: "Action",
      key: "action",
      width: "8%",
      render: (book) => {
        const handleReturnBook = async () => {
          try {
            await API.post(
              `/api/Book/${book.bookId}/return`,
              {},
              { params: { userId: user?.id } }
            );
            notification.success({ message: "Book returned!" });
            await getBooks();
          } catch (error) {
            notification.error({ message: "Failed to return book!" });
            console.log(error);
          }
        };

        const handlePayFine = async () => {
          try {
            await API.post(
              `/api/User/${user?.id}/process-fine`,
              {},
              { params: { bookId: book.bookId } }
            );
            notification.success({ message: "Successfully payed fine!" });
            await getBooks();
          } catch (error) {
            notification.error({ message: "Failed to pay fine!" });
            console.log(error);
          }
        };

        return book.hasFines ? (
          <Button
            shape="circle"
            type="text"
            onClick={handlePayFine}
            color="red"
            danger
          >
            Pay fine
          </Button>
        ) : (
          <Button shape="circle" type="text" onClick={handleReturnBook}>
            <ArrowRightOutlined />
          </Button>
        );
      },
    },
  ];

  const data = books.map((book) => {
    return {
      key: book.id,
      bookName: book.bookName,
      author: book.author,
      genre: book.genre,
      rentPrice: book.rentPrice,
      transferDate: moment(book.transferDate).format("DD/MM/YYYY hh:mm"),
      expectedReturnDate: moment(book.expectedReturnDate).format(
        "DD/MM/YYYY hh:mm:ss"
      ),
      overdue: moment.now() > book.expectedReturnDate,
      hasFines: book.hasFines,
      bookId: book.bookId,
    };
  });

  return (
    <div className="mb-lg-4">
      <Title level={2}>Rented books</Title>
      <Table
        columns={columns}
        dataSource={data}
        pagination={false}
        loading={loading}
      />
    </div>
  );
};

export default RentedBooks;
