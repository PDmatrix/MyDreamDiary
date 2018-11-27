import { Divider, Spin } from "antd";
import React, { useState } from "react";
import CreateDream from "../../components/User/CreateDream";
import DreamList from "../../components/User/DreamList";
import UserInfo from "../../components/User/UserInfo";
import Auth from "../../lib/Auth";
import useFetch from "../../lib/useFetch";

interface IUserPostInterface {
  id: number;
  title: string;
}

interface IUserInterface {
  name: string;
  email: string;
  id: string;
  comments: ICommentInterface[];
  posts: IUserPostInterface[];
  dreams: IDreamInterface[];
}

const User: React.FunctionComponent = () => {
  const auth = Auth.getInstance();
  const { data, loading, setData, setLoading } = useFetch<IUserInterface>(
    `${process.env.API_URL}/api/user/${Auth.getUserId()}`,
    "get"
  );
  const [dreams, setDreams] = useState([]);
  const handleInput = (newDream: IDreamInterface) => {
    const newData = data;
    setLoading(true);
    newData.dreams = [...data.dreams, newDream];
    setDreams(newData.dreams);
    setData(newData);
    setLoading(false);
  };

  if (!Auth.isAuthenticated()) {
    auth.login();
  }

  if (!loading && dreams !== data.dreams) {
    setDreams(data.dreams);
  }

  return (
    <div>
      {!loading ? (
        <>
          {Auth.isAuthenticated() && (
            <>
              <UserInfo email={data.email} name={data.name} />
              <CreateDream handleInput={handleInput} />
              <Divider />
              {data.dreams.length > 0 && (
                <>
                  <h3>Список снов:</h3>
                  <DreamList dreams={dreams} />
                </>
              )}
            </>
          )}
        </>
      ) : (
        <Spin />
      )}
    </div>
  );
};

export default User;
