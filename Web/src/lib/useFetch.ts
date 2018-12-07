import axios from "axios";
import { Dispatch, useEffect, useState } from "react";

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
    const res = await axios({
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
