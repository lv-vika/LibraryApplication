import React, {useState} from 'react';
import {Menu, Layout as AntdLayout, theme, Dropdown, Button, Statistic, Space} from "antd";
import {useAuth} from "../../hooks/useAuth";
import {useUser} from "../../hooks/useUser";
import {useHistory} from "react-router-dom";
import TopUpModal from "../TopUpModal";

const { Header, Content, Footer } = AntdLayout;

function UserOutlined() {
    return null;
}

const Layout = ({children}) =>  {
  const {
      token: { colorBgLayout },
  } = theme.useToken();
    const { logout } = useAuth();
    const { user } = useUser();
    const history = useHistory();

    const [showTopUpModal, setShowTopUpModal] = useState(false)

    const userMenuItems = [
        {
            key: 'logout',
            label: 'Logout',
            onClick: () => logout()
        },
    ]

    const navMenuItems = [
        {key: "catalog", label: "Catalog", onClick: () => history.push('/catalog')},
        {key: "profile", label: "Profile", onClick: () => history.push('/profile')},
    ]

  return (
      <AntdLayout theme="light" style={{ minHeight: '100vh' }}>
          {user ? <Header
              className='d-flex-space-between'
              style={{
                  backgroundColor: '#fff',
                  boxShadow: '0 1px 4px rgba(0, 21, 41, 0.08)',
                  width: '100%',
              }}
          >
              <Menu
                  theme="light"
                  mode="horizontal"
                  items={navMenuItems}
                  style={{width: 240}}
              />
              <Space size={16}>
                  <Button type="primary" size='large' onClick={() => setShowTopUpModal(true)}>Top up</Button>
                  {showTopUpModal && <TopUpModal setShowModal={setShowTopUpModal} />}
                <Statistic title="Balance" value={user?.balance} prefix='$' />
                <Dropdown
                    menu={{items: userMenuItems}}
                    overlayStyle={{minWidth: 180}}
                >
                    <Button type="text" size='large'>
                        <UserOutlined /> {user?.name}
                    </Button>
                </Dropdown>
              </Space>
          </Header> : null}
          <Content
              style={{
                  padding: '24px 48px',
              }}
          >
              <div
                  className="site-layout-content"
                  style={{
                      background: colorBgLayout,
                  }}
              >
                  {children}
              </div>
          </Content>
          <Footer
              style={{
                  textAlign: 'center',
              }}
          >
              Library
          </Footer>
      </AntdLayout>
  )
}
export default Layout;
