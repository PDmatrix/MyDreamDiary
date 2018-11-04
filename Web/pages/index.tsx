import React, { useState } from "react";
import axios from "axios";
import Next from "next";

const Index : Next.NextSFC = () => {
    const [id, setId] = useState(0);
    return (
        <>
            {id}
            <button onClick={() => setId(id + 1)}>Click</button>
        </>
    );
};

Index.getInitialProps = async () => {
    const res = await axios.get("https://api.github.com/repos/zeit/next.js");
    return { elems : res.data };
};

export default Index;