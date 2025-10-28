import {useEffect, useState} from 'react'
import './App.css'
import type {Post} from "./generated-ts-client.ts";
import {SieveQueryBuilder} from "ts-sieve-query-builder";
import {httpClient} from "./httpClient.ts";

function App() {

    const [posts, setPosts] = useState<Post[]>([]);
    const [searchForTitle, setSearchForTitle] = useState<string>("")

    useEffect(() => {
        const query = SieveQueryBuilder.create<Post>()
            .filterContains("title", searchForTitle)
            .buildSieveModel();

        httpClient.getPosts(query).then(result => {
            setPosts(result)
        })
    }, [searchForTitle])

    return (
        <>
            <input placeholder="post title must contain" onChange={e => setSearchForTitle(e.target.value)}/>
            {
                JSON.stringify(posts)
            }
        </>
    )
}

export default App
