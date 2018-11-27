import axios from "axios";
import { Dispatch, useEffect, useState } from "react";
import Auth from "./Auth";

const useFetch = <T extends {}>(
  url: string,
  verb: string
): {
  data: T;
  loading: boolean;
  setData: Dispatch<any>;
  setLoading: Dispatch<any>;
} => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  const loadData = async () => {
    const authorization = "Bearer " + Auth.getUserToken();
    const res = await axios({
      headers: { Authorization: authorization },
      method: verb,
      url
    });
    setData(await res.data);
    setLoading(false);
  };

  useEffect(() => {
    loadData().then();
  }, []);

  return { data, loading, setData, setLoading };
};

export default useFetch;
