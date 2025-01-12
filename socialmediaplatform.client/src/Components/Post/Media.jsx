import React from 'react';

const Media = ({ mediaUrl, contentType }) => {
    return (
        <>
            {contentType === 'image' && (
                <img src={mediaUrl} alt="Post media" className="mediaImage" />
            )}
            {contentType === 'video' && (
                <video controls className="mediaVideo">
                    <source src={mediaUrl} type="video/mp4" />
                    Your browser does not support the video tag.
                </video>
            )}
        </>
    );
};

export default Media;
