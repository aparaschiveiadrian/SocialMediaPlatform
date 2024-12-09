import React from 'react';

const Media = ({ mediaUrl, contentType }) => {
    if (contentType === "photo") {
        return <img src={mediaUrl} alt="Source" className="postMedia" />;
    } else if (contentType === "video") {
        return <video src={mediaUrl} controls className="postMedia" />;
    }
    return null;
};

export default Media;
